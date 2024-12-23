using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Persistence;

namespace MbdcLocalBudgetsDomain.Services;

public interface IAnnualBudgetReportService
{
    Task<Result> Upload(MemoryStream report, int year, string city, CancellationToken ct = default);
    Task<Result<AnnualBudgetReport>> GetById(string id, CancellationToken ct = default);
    Task Add(AnnualBudgetReport record, CancellationToken ct = default);
}