using AutoMapper;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenRepository(EBayDbContext _context, IMapper _mapper) : IRefreshTokenRepository
{
    public async Task AddRefreshToken(RefreshTokenEntity tokenEntity)
    {
        var entity = new RefreshToken
        {
            Token = tokenEntity.Token,
            ExpiresAt = tokenEntity.ExpiresAt,
            UserId = tokenEntity.UserId,
            CreatedAt = tokenEntity.CreatedAt
        };
        await _context.RefreshTokens.AddAsync(entity);
    }

    public async Task<RefreshTokenEntity?> GetByTokenAndUserId(string token, int userId)
    {
        var rfToken = await _context.RefreshTokens.SingleOrDefaultAsync(rf => rf.Token == token && rf.UserId == userId);
        var rfTokenMapper = _mapper.Map<RefreshTokenEntity>(rfToken);
        return rfTokenMapper;
    }

    public async Task DeleteTokenById(long id)
    {
        var entity = await _context.RefreshTokens.FindAsync(id);
        if (entity is not null)
        {
            _context.RefreshTokens.Remove(entity);
        }
    }

}