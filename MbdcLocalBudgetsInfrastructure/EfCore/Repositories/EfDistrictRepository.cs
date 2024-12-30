using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Repositories;

public class EfDistrictRepository : IDistrictRepository
{
    private readonly OlapDbContext _context;

    public EfDistrictRepository(OlapDbContext context)
    {
        _context = context;
    }
    public async Task<District> Add(District district, CancellationToken ct = default)
    {
        await _context.Districts.AddAsync(district, ct);
        return district;
    }

    public async Task Add(IEnumerable<District> districts, CancellationToken ct = default)
    {
        await _context.Districts.AddRangeAsync(districts, ct);
    }

    public async Task<District?> GetById(Guid id, CancellationToken ct = default)
    {
        return await _context.Districts.FindAsync(new object[] { id }, ct);
    }

    public async Task<List<District>> GetAll(CancellationToken ct = default)
    {
        return await _context.Districts.ToListAsync(ct);
    }
}