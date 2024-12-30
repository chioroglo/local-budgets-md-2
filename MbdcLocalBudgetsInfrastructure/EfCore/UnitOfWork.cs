using MbdcLocalBudgetsDomain.Persistence;

namespace MbdcLocalBudgetsInfrastructure.EfCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly OlapDbContext _dbContext;

    public UnitOfWork(OlapDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var affectedRows = await _dbContext.SaveChangesAsync(ct);
        return affectedRows;
    }
}