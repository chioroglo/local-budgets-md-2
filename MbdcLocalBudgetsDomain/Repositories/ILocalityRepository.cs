using MbdcLocalBudgetsDomain.Entities.Olap;

namespace MbdcLocalBudgetsDomain.Repositories;

public interface ILocalityRepository
{
    Task<Locality> Add(Locality locality, CancellationToken ct = default);
    Task Add(List<Locality> localities, CancellationToken ct = default);
    Task<Locality?> GetById(Guid id, CancellationToken ct = default);
    Task<List<Locality>> GetAll(CancellationToken ct = default);
}