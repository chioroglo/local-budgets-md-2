namespace MbdcLocalBudgetsDomain.Entities.Olap;

public class District : EfBaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public LocalityStatus Status { get; set; }
    public ICollection<Locality> Localities { get; set; }
    public ICollection<OlapBudgetReport> BudgetReports { get; set; }
}