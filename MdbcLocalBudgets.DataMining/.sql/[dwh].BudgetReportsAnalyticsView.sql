CREATE OR ALTER VIEW [dwh].BudgetReportsAnalyticsView AS
SELECT
	l.*,
	obr.*,
	 CAST(CONCAT(obr.YearId, '-01-01') AS DATE) AS [Year]
FROM [dwh].OlapBudgetReport obr JOIN [dwh].Locality l ON l.Id = obr.LocalityId