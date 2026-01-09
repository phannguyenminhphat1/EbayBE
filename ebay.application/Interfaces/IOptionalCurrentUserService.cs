using ebay.domain.Entities;

public interface IOptionalCurrentUserService
{
    int? UserId { get; }
    bool IsAuthenticated { get; }
}
