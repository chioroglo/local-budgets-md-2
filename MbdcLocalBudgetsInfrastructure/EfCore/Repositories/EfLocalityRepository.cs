using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Repositories;

public class EfLocalityRepository : ILocalityRepository
{
    private readonly OlapDbContext _context;

    public EfLocalityRepository(OlapDbContext context)
    {
        _context = context;
    }

    public async Task<Locality> Add(Locality locality, CancellationToken ct = default)
    {
        await _context.Localities.AddAsync(locality, ct);
        return locality;
    }

    public async Task Add(List<Locality> localities, CancellationToken ct = default)
    {
        await _context.Localities.AddRangeAsync(localities, ct);
    }

    public async Task<Locality?> GetById(Guid id, CancellationToken ct = default)
    {
        return await _context.Localities.FindAsync([id], cancellationToken: ct);
    }

    public async Task<List<Locality>> GetAll(CancellationToken ct = default)
    {
        return await _context.Localities.ToListAsync(ct);
    }
}