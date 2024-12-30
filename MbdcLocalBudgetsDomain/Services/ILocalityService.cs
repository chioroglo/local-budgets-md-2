using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Persistence;

namespace MbdcLocalBudgetsDomain.Services;

public interface ILocalityService
{
    Task<Result<Locality?>> GetById(Guid id, CancellationToken ct = default);
    Task<Result> Upload(MemoryStream file, CancellationToken ct = default);
}