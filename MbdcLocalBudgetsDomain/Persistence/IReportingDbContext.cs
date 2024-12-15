using MbdcLocalBudgetsDomain.Entities;
using MongoDB.Driver;

namespace MbdcLocalBudgetsDomain.Persistence;

public interface IReportingDbContext
{
    IMongoCollection<AnnualBudgetReport> AnnualBudgetReports { get; }
}