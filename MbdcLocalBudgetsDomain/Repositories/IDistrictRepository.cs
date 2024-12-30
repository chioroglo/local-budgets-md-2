using MbdcLocalBudgetsDomain.Entities.Olap;

namespace MbdcLocalBudgetsDomain.Repositories;

public interface IDistrictRepository
{
    Task<District> Add(District district, CancellationToken ct = default);
    Task Add(IEnumerable<District> districts, CancellationToken ct = default);
    Task<District?> GetById(Guid id, CancellationToken ct = default);
    Task<List<District>> GetAll(CancellationToken ct = default);
}