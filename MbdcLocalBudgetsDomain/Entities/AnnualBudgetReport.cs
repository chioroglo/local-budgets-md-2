using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MbdcLocalBudgetsDomain.Entities;

public class AnnualBudgetReport
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string City { get; set; }
    public int Year { get; set; }
    public long PlannedExpenses { get; set; }
    public long ActualExpenses { get; set; }
    public long PlannedIncome { get; set; }
    public long ActualIncome { get; set; }
}