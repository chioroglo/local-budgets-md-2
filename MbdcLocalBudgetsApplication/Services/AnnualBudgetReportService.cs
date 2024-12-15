using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsDomain.Repositories;
using MbdcLocalBudgetsDomain.Services;

namespace MbdcLocalBudgetsApplication.Services;

public class AnnualBudgetReportService : IAnnualBudgetReportService
{
    private readonly IAnnualBudgetReportRepository _annualBudgetReportRepository;

    public AnnualBudgetReportService(IAnnualBudgetReportRepository annualBudgetReportRepository)
    {
        _annualBudgetReportRepository = annualBudgetReportRepository;
    }
    public async Task<Result<AnnualBudgetReport>> GetById(string id, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            return Result.Failure<AnnualBudgetReport>(Error.NullValue);
        }

        return await _annualBudgetReportRepository.GetById(id, ct);
    }

    public async Task Add(AnnualBudgetReport record, CancellationToken ct = default)
    {
        await _annualBudgetReportRepository.Add(record, ct);
    }
}