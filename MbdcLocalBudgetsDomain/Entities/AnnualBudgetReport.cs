using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MbdcLocalBudgetsDomain.Entities;

public class AnnualBudgetReport
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string City { get; set; }
    public int Year { get; set; }
    public long PlannedExpenses { get; set; }
    public long ActualExpenses { get; set; }
    public long PlannedIncome { get; set; }
    public long ActualIncome { get; set; }
    public long Population { get; set; }
    public ExpensesDetails ExpenseDetails { get; set; } = new();
    public IncomesDetails IncomeDetails { get; set; } = new();
    public class ExpensesDetails
    {
        // Compensarea angajaților
        public long SalariesPaid { get; set; }
        // Mijloace fixe
        public long FixedAssets { get; set; }
        // Utilizarea bunurilor și serviciilor
        public long UtilityServices { get; set; }
        // Protectie sociala
        public long PublicWelfare { get; set; }
        // Cultura, sport, tineret, culte si odihna
        public long CultureInvestments { get; set; }
    }
    public class IncomesDetails
    {
        public TaxesDetailed Taxes { get; set; } = new();
        public class TaxesDetailed
        {
            // Impozite asupra veniturilor, profiturilor și câștigurilor de capital
            public long AssetIncomeTax { get; set; }
            // Impozitele pe proprietate
            public long PropertyTax { get; set; }
            // Taxa pentru patenta de intreprinzinzator
            public long EntrepreneurshipTax { get; set; }
            // Taxa pentru amenajarea teritoriului
            public long LandUseTax { get; set; }
        }
        // Vânzări de bunuri și servicii
        public long GoodsAndServiceIncome { get; set; }
        // Chirie
        public long PropertyRentIncome { get; set; }
    }
}