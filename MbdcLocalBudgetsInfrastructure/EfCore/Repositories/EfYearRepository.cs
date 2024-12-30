using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Repositories;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Repositories;

public class EfYearRepository : IYearRepository
{
    private readonly OlapDbContext _context;

    public EfYearRepository(OlapDbContext context)
    {
        _context = context;
    }

    public async Task<Year> Add(Year year, CancellationToken ct = default)
    {
        await _context.AddAsync(year, ct);
        return year;
    }
}