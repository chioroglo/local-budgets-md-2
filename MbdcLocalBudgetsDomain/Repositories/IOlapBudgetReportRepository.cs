using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Models;

namespace MbdcLocalBudgetsDomain.Repositories;

public interface IOlapBudgetReportRepository
{
    Task Add(IEnumerable<OlapBudgetReport> reports, CancellationToken ct = default);
    Task<DateTime> GetLastImportDate(CancellationToken ct = default);

    Task<IEnumerable<OlapBudgetReportDefinitionModel>> WeedOutExistingReports(
        IEnumerable<OlapBudgetReportDefinitionModel> models, CancellationToken ct = default);
}