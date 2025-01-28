using System.Collections.Frozen;
using MbdcLocalBudgetsDomain.Entities;

namespace MbdcLocalBudgetsDomain.Helpers;

public static class BudgetReportParsingHelper
{
    public static FrozenDictionary<string, Action<AnnualBudgetReport, long>> Categories = new Dictionary<string, Action<AnnualBudgetReport, long>>()
    {
        { "Cheltuieli aprobate", (report, value) => report.PlannedExpenses = value },
        { "Cheltuieli executate", (report, value) => report.ActualExpenses = value },
        { "Venituri aprobate", (report, value) => report.PlannedIncome = value },
        { "Venituri executate", (report, value) => report.ActualIncome = value },
        { "Numărul de locuitori", (report, value) => report.Population = value },
        { "NumÄƒrul de locuitori", (report, value) => report.Population = value },
        { "Compensarea angajaților", (report, value) => report.ExpenseDetails.SalariesPaid = value },
        { "Compensarea angajaÈ›ilor", (report, value) => report.ExpenseDetails.SalariesPaid = value },
        { "Mijloace fixe", (r,v) => r.ExpenseDetails.FixedAssets = v },
        { "Utilizarea bunurilor și serviciilor", (r,v) => r.ExpenseDetails.UtilityServices = v },
        { "Utilizarea bunurilor È\u2122i serviciilor", (r,v) => r.ExpenseDetails.UtilityServices = v },
        { "Protectie sociala", (r,v) => r.ExpenseDetails.PublicWelfare = v },
        { "Cultura, sport, tineret, culte si odihna", (r,v) => r.ExpenseDetails.CultureInvestments = v },
        { "Impozite asupra veniturilor, profiturilor și câștigurilor de capital", (r,v) => r.IncomeDetails.Taxes.AssetIncomeTax = v },
        { "Impozite asupra veniturilor, profiturilor È\u2122i cÃ\u00a2È\u2122tigurilor de capital", (r,v) => r.IncomeDetails.Taxes.AssetIncomeTax = v },
        { "Impozitele pe proprietate", (r,v) => r.IncomeDetails.Taxes.PropertyTax = v },
        { "Taxa pentru patenta de intreprinzinzator", (r,v) => r.IncomeDetails.Taxes.EntrepreneurshipTax = v },
        { "Taxa pentru amenajarea teritoriului" , (r,v) => r.IncomeDetails.Taxes.LandUseTax = v },
        { "Vânzări de bunuri și servicii", (r,v) => r.IncomeDetails.GoodsAndServiceIncome = v },
        { "VÃ\u00a2nzÄƒri de bunuri È\u2122i servicii", (r,v) => r.IncomeDetails.GoodsAndServiceIncome = v },
        { "Chirie", (r,v) => r.IncomeDetails.PropertyRentIncome = v }

    }.ToFrozenDictionary();
}