import pyodbc;
import pandas;


class DatabaseService:
    @staticmethod
    def fetch_training_data(connection_string: str) -> pandas.DataFrame:
        connection = pyodbc.connect(connection_string)

        query = """
            SELECT
                PlannedExpenses,
                ActualExpenses,
                PlannedIncome,
                ActualIncome,
                ExpensesDetailsSalariesPaid,
                ExpensesDetailsFixedAssets,
                ExpensesDetailsUtilityServices,
                ExpensesDetailsPublicWelfare,
                ExpensesDetailsCultureInvestments,
                LocalityPopulation,
                DistrictCode,
                LocalityCode
            FROM [dwh].BudgetReportsModelView;
        """

        data = pandas.read_sql(query, connection)
        connection.close()
        return data