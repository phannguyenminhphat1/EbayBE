public interface IUnitOfWork : IAsyncDisposable
{

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();

    Task<int> SaveChangesAsync();
    Task RollBackTransactionAsync();
}