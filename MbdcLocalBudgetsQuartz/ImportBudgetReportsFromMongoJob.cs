using MbdcLocalBudgetsDomain.Services;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MbdcLocalBudgetsQuartz;

[DisallowConcurrentExecution]
public class ImportBudgetReportsFromMongoJob : IJob
{
    private readonly ILogger<ImportBudgetReportsFromMongoJob> _logger;
    private readonly IOlapBudgetReportService _service;

    public ImportBudgetReportsFromMongoJob(ILogger<ImportBudgetReportsFromMongoJob> logger,
        IOlapBudgetReportService service)
    {
        _logger = logger;
        _service = service;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        var now = context.FireTimeUtc.DateTime;
        _logger.LogInformation("Began importing {0}", DateTime.UtcNow);

        await _service.SyncDataWarehouse(now, context.CancellationToken);
    }
}