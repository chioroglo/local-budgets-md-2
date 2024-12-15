using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Persistence;

namespace MbdcLocalBudgetsDomain.Services;

public interface IAnnualBudgetReportService
{
    Task<Result<AnnualBudgetReport>> GetById(string id, CancellationToken ct = default);
    Task Add(AnnualBudgetReport record, CancellationToken ct = default);
}