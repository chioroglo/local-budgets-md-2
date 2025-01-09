using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Entities.Olap;

namespace MbdcLocalBudgetsDomain.Models;

public class OlapBudgetReportDefinitionModel
{
    public OlapBudgetReportDefinitionModel()
    {
        
    }

    public OlapBudgetReportDefinitionModel(OlapBudgetReport report)
    {
        Year = report.YearId;
        LocalityId = report.LocalityId;
        DistrictId = report.DistrictId;
    }

    public int Year { get; set; }
    public Guid LocalityId { get; set; }
    public Guid DistrictId { get; set; }
}