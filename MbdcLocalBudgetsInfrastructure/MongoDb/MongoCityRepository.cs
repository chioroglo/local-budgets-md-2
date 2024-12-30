using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsDomain.Repositories;
using MongoDB.Driver;

namespace MbdcLocalBudgetsInfrastructure.MongoDb;

public class MongoCityRepository : ICityRepository
{
    private readonly IReportingDbContext _context;

    public MongoCityRepository(IReportingDbContext reportingDbContext)
    {
        _context = reportingDbContext;
    }

    public async Task<bool> DoesNotExist(string cityName, CancellationToken ct = default)
    {
        var query = Builders<City>.Filter.Eq(r => r.Name, cityName);
        return await _context.Cities.CountDocumentsAsync(query, cancellationToken: ct) == 0;
    }

    public async Task Add(City city, CancellationToken ct = default)
    {
        await _context.Cities.InsertOneAsync(city, new InsertOneOptions
        {
            BypassDocumentValidation = false,
        }, ct);
    }
}