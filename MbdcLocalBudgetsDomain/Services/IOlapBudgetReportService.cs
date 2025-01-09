namespace MbdcLocalBudgetsDomain.Services;

public interface IOlapBudgetReportService
{
    Task SyncDataWarehouse(DateTime now, CancellationToken сt);
}