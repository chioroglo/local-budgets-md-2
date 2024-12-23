using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Persistence;
using MongoDB.Driver;

namespace MbdcLocalBudgetsInfrastructure.MongoDb;

public class MongoReportingDbContext : IReportingDbContext
{
    private readonly IMongoDatabase _database;

    public MongoReportingDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<AnnualBudgetReport> AnnualBudgetReports => _database.GetCollection<AnnualBudgetReport>("annualBudgetReports");
    public IMongoCollection<City> Cities => _database.GetCollection<City>("cities");
}