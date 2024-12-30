namespace MbdcLocalBudgetsDomain.Entities.Olap;

public class Year
{
    public Year(int value)
    {
        Value = value;
    }

    public int Value { get; set; }
    public ICollection<OlapBudgetReport> BudgetReports { get; set; }

}