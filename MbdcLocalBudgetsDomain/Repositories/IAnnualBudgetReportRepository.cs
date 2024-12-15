using MbdcLocalBudgetsDomain.Entities;

namespace MbdcLocalBudgetsDomain.Repositories;

public interface IAnnualBudgetReportRepository
{
    Task<AnnualBudgetReport> GetById(string id, CancellationToken ct = default);
    Task Add(AnnualBudgetReport record, CancellationToken ct = default);
}