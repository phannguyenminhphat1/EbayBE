using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly string? _key;
    private readonly string? _issuer;
    private readonly string? _audience;
    private readonly EBayDbContext _context;
    public JwtService(IConfiguration configuration, EBayDbContext db)
    {
        _key = configuration.GetValue<string>("AppSettings:Key");
        _issuer = configuration.GetValue<string>("AppSettings:Issuer");
        _audience = configuration.GetValue<string>("AppSettings:Audience");
        _context = db;
    }

    #region DECODE TOKEN
    public string DecodeToken(string token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token không được để trống", nameof(token));
            }

            // Tạo handler và đọc token
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Lấy username từ claims (thường nằm trong claim "sub" hoặc "name")
            var usernameClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "Username"); // Common in some identity providers

            if (usernameClaim == null)
            {
                throw new InvalidOperationException("Không tìm thấy username trong payload");
            }

            return usernameClaim.Value;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Lỗi khi decode token: {ex.Message}", ex);
        }
    }
    #endregion



    #region GENERATE REFRESH TOKEN

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    #endregion


    #region GENERATE TOKEN

    public string GenerateToken(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.FullName!),

        };
        var userRoles = _context.UserRoles
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == user.Id)
            .Select(ur => ur.Role.RoleName)
            .ToList();

        foreach (var role in userRoles)// duyệt qua từng role
        {
            claims.Add(new Claim(ClaimTypes.Role, role.ToString())); // thêm claim Role
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds

        );
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    #endregion


    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}