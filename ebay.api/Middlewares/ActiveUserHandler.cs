using System.Security.Claims;
using System.Text.Json;
using ebay.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

public class ActiveUserRequirement : IAuthorizationRequirement { }
public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
{
    private readonly IUserRepository _userRepository;

    public ActiveUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            context.Fail(new AuthorizationFailureReason(this, "User ID claim is missing."));
            return;
        }
        if (!int.TryParse(userIdClaim, out var userId))
        {
            context.Fail(new AuthorizationFailureReason(this, "User ID claim is invalid."));
            return;
        }
        var user = await _userRepository.FindUserById(userId);

        if (user == null || user.Deleted == true)
        {
            Console.WriteLine("3");
            System.Console.WriteLine("Vào đây rồi đúng không ?: " + JsonSerializer.Serialize(user));

            context.Fail(new AuthorizationFailureReason(this, "User not found."));
            return;
        }
        context.User.AddIdentity(new ClaimsIdentity(new[]
        {
            new Claim("User", JsonSerializer.Serialize(user))
        }));
        context.Succeed(requirement);
    }
}
