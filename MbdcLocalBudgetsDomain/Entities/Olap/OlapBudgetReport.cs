namespace MbdcLocalBudgetsDomain.Entities.Olap;

public class OlapBudgetReport
{
    public Year Year { get; set; }
    public int YearId { get; set; }
    public Locality Locality { get; set; }
    public Guid LocalityId { get; set; }
    public District District { get; set; }
    public Guid DistrictId { get; set; }
    public long PlannedExpenses { get; set; }
    public long ActualExpenses { get; set; }
    public long PlannedIncome { get; set; }
    public long ActualIncome { get; set; }
    public long ExpensesDetailsSalariesPaid { get; set; }          // Compensarea angajaților
    public long ExpensesDetailsFixedAssets { get; set; }           // Mijloace fixe
    public long ExpensesDetailsUtilityServices { get; set; }       // Utilizarea bunurilor și serviciilor
    public long ExpensesDetailsPublicWelfare { get; set; }         // Protecție socială
    public long ExpensesDetailsCultureInvestments { get; set; }    // Cultură, sport, tineret, culte și odihnă
    public long IncomesDetailsTaxesAssetIncomeTax { get; set; }   // Impozite asupra veniturilor, profiturilor și câștigurilor de capital
    public long IncomesDetailsTaxesPropertyTax { get; set; }      // Impozitele pe proprietate
    public long IncomesDetailsTaxesEntrepreneurshipTax { get; set; } // Taxa pentru patentă de întreprinzător
    public long IncomesDetailsTaxesLandUseTax { get; set; }       // Taxa pentru amenajarea teritoriului
    public long IncomesDetailsGoodsAndServiceIncome { get; set; }  // Vânzări de bunuri și servicii
    public long IncomesDetailsPropertyRentIncome { get; set; }     // Chirie
}