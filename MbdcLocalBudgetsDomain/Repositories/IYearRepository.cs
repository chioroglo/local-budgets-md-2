using System.ComponentModel;
using MbdcLocalBudgetsDomain.Entities.Olap;

namespace MbdcLocalBudgetsDomain.Repositories;

public interface IYearRepository
{
    Task<Year> Add(Year year, CancellationToken ct = default); 
}