namespace MbdcLocalBudgetsDomain.Persistence;

public interface IUnitOfWork
{
    /// <returns>Number of affected records</returns>
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}