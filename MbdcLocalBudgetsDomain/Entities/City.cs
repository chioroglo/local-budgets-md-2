namespace MbdcLocalBudgetsDomain.Entities;

public class City : MongoBaseEntity
{
    public string Name { get; set; }
    public string District { get; set; }
    public int Population { get; set; }
}