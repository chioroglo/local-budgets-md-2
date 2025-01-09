using System.Globalization;
using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Models;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsDomain.Repositories;
using MbdcLocalBudgetsDomain.Services;


namespace MbdcLocalBudgetsApplication.Services;

public class OlapBudgetReportService : IOlapBudgetReportService 
{
    private readonly IAnnualBudgetReportRepository _annualBudgetReportRepository;
    private readonly IOlapBudgetReportRepository _olapBudgetReportRepository;
    private readonly ILocalityRepository _localityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OlapBudgetReportService(IAnnualBudgetReportRepository annualBudgetReportRepository,
        IOlapBudgetReportRepository olapBudgetReportRepository,
        ILocalityRepository localityRepository,
        IUnitOfWork unitOfWork)
    {
        _annualBudgetReportRepository = annualBudgetReportRepository;
        _olapBudgetReportRepository = olapBudgetReportRepository;
        _localityRepository = localityRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task SyncDataWarehouse(DateTime now, CancellationToken ct)
    {
        var latestImportTimestamp = await _olapBudgetReportRepository.GetLastImportDate(ct);
        var newReports = await _annualBudgetReportRepository.GetReportsImportedAfter(latestImportTimestamp, ct);
        var localities = await _localityRepository.GetAll(ct);

        var olapModels = newReports.Select(nr =>
        {
            var locality = localities.FirstOrDefault(e =>
                string.Compare(e.Name, nr.City, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (locality == null)
            {
                return null;
            }
            var district = locality.ParentDistrict;

            if (locality.Population == null || locality.Population <= 0)
            {
                locality.Population = nr.Population;
            }

            return new OlapBudgetReport
            {
                YearId = nr.Year,
                LocalityId = locality.Id,
                DistrictId = district.Id,
                PlannedExpenses = nr.PlannedExpenses,
                ActualExpenses = nr.ActualExpenses,
                PlannedIncome = nr.PlannedIncome,
                ActualIncome = nr.ActualIncome,
                ExpensesDetailsSalariesPaid = nr.ExpenseDetails.SalariesPaid,
                ExpensesDetailsFixedAssets = nr.ExpenseDetails.FixedAssets,
                ExpensesDetailsUtilityServices = nr.ExpenseDetails.UtilityServices,
                ExpensesDetailsPublicWelfare = nr.ExpenseDetails.PublicWelfare,
                ExpensesDetailsCultureInvestments = nr.ExpenseDetails.CultureInvestments,
                IncomesDetailsTaxesAssetIncomeTax = nr.IncomeDetails.Taxes.AssetIncomeTax,
                IncomesDetailsTaxesPropertyTax = nr.IncomeDetails.Taxes.PropertyTax,
                IncomesDetailsTaxesEntrepreneurshipTax = nr.IncomeDetails.Taxes.EntrepreneurshipTax,
                IncomesDetailsTaxesLandUseTax = nr.IncomeDetails.Taxes.LandUseTax,
                IncomesDetailsGoodsAndServiceIncome = nr.IncomeDetails.GoodsAndServiceIncome,
                IncomesDetailsPropertyRentIncome = nr.IncomeDetails.PropertyRentIncome
            };
        }).Where(e => e != null);


        var definitionModels = olapModels.Select(e => new OlapBudgetReportDefinitionModel((OlapBudgetReport)e));
        var reportIdsShouldBeImported = await _olapBudgetReportRepository.WeedOutExistingReports(definitionModels, ct);

        olapModels = olapModels.Where(om =>
            reportIdsShouldBeImported.Any(obrdm => om.YearId == obrdm.Year && om.DistrictId == obrdm.DistrictId && obrdm.LocalityId == om.LocalityId)
            );

        await _olapBudgetReportRepository.Add(olapModels, ct);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}