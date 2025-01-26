CREATE OR ALTER VIEW [dwh].[TaxIncomePiechart] AS
	SELECT
	brav.[Name],
	brav.[Year],
	brav.[IncomesDetailsTaxesAssetIncomeTax],
	brav.[IncomesDetailsTaxesPropertyTax],
	brav.[IncomesDetailsTaxesEntrepreneurshipTax],
	brav.[IncomesDetailsTaxesLandUseTax],
	brav.[IncomesDetailsGoodsAndServiceIncome],
	brav.[IncomesDetailsPropertyRentIncome]
	FROM [dwh].[BudgetReportsAnalyticsView] brav;