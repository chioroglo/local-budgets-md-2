using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsDomain.Repositories;
using MongoDB.Driver;

namespace MbdcLocalBudgetsApplication.Repositories;

public class MongoAnnualBudgetReportRepository : IAnnualBudgetReportRepository
{
    private readonly IReportingDbContext _context;

    public MongoAnnualBudgetReportRepository(IReportingDbContext context)
    {
        _context = context;
    }
    public async Task<AnnualBudgetReport?> GetById(string id, CancellationToken ct = default)
    {
        return await _context.AnnualBudgetReports
            .Find(Builders<AnnualBudgetReport>.Filter.Eq(p => p.Id, id))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ReportAlreadyExists(int year, string city, CancellationToken ct = default)
    {
        var query = Builders<AnnualBudgetReport>.Filter.And(
            Builders<AnnualBudgetReport>.Filter.Eq(r => r.City, city),
            Builders<AnnualBudgetReport>.Filter.Eq(r => r.Year, year));

        return await _context.AnnualBudgetReports.CountDocumentsAsync(query, cancellationToken: ct) != 0;
    }

    public async Task Add(AnnualBudgetReport record, CancellationToken ct = default)
    {
        await _context.AnnualBudgetReports.InsertOneAsync(record, cancellationToken: ct);
    }
}