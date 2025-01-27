CREATE OR ALTER VIEW [dwh].BudgetReportsModelView AS
SELECT
[obr].PlannedExpenses,
[obr].ActualExpenses,
[obr].PlannedIncome,
[obr].ActualIncome,
[obr].ExpensesDetailsSalariesPaid,
[obr].ExpensesDetailsFixedAssets,
[obr].ExpensesDetailsUtilityServices,
[obr].ExpensesDetailsPublicWelfare,
[obr].ExpensesDetailsCultureInvestments,
[obr].IncomesDetailsTaxesAssetIncomeTax,
[obr].IncomesDetailsTaxesPropertyTax,
[obr].IncomesDetailsTaxesEntrepreneurshipTax,
[obr].IncomesDetailsTaxesLandUseTax,
[obr].IncomesDetailsGoodsAndServiceIncome,
[obr].IncomesDetailsPropertyRentIncome,
[l].Code AS LocalityCode,
[l].[Population] AS LocalityPopulation,
[d].Code AS DistrictCode
FROM [dwh].OlapBudgetReport obr JOIN [dwh].Locality l ON l.Id = obr.LocalityId JOIN [dwh].District d ON d.Id = l.ParentDistrictId
