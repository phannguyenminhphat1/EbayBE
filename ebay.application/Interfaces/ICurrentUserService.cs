using ebay.domain.Entities;

public interface ICurrentUserService
{
    int UserId { get; }
    List<string> Roles { get; }
}
