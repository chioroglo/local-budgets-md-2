using MbdcLocalBudgetsDomain.Entities;

namespace MbdcLocalBudgetsDomain.Repositories;

public interface ICityRepository
{
    Task<bool> DoesNotExist(string cityName, CancellationToken ct = default);
    Task Add(City city, CancellationToken ct = default);
}