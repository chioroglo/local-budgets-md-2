using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Models;
using MbdcLocalBudgetsDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Repositories;

public class OlapBudgetReportRepository : IOlapBudgetReportRepository
{
    private readonly OlapDbContext _dbContext;

    public OlapBudgetReportRepository(OlapDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(IEnumerable<OlapBudgetReport> reports, CancellationToken ct = default)
    {
        await _dbContext.OlapBudgetReports.AddRangeAsync(reports, ct);
    }

    public async Task<DateTime> GetLastImportDate(CancellationToken ct = default)
    {
        return await _dbContext.OlapBudgetReports
            .OrderBy(e => e.Timestamp)
            .Select(o => o.Timestamp)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<OlapBudgetReportDefinitionModel>> WeedOutExistingReports(IEnumerable<OlapBudgetReportDefinitionModel> models, CancellationToken ct = default)
    {
        var existingReports = await _dbContext.OlapBudgetReports
            .Select(obr => new OlapBudgetReportDefinitionModel
            {
                Year = obr.YearId,
                LocalityId = obr.LocalityId,
                DistrictId = obr.DistrictId
            })
            .ToListAsync(ct);

        var weptOutModels = models.Where(obrdm => 
            !existingReports.Any(er => er.Year == obrdm.Year &&
                                      er.DistrictId == obrdm.DistrictId &&
                                      er.LocalityId == obrdm.LocalityId)
            );
        return weptOutModels;
    }
}