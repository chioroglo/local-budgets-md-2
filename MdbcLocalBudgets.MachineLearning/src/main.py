import sys
from database_service import DatabaseService


connection_string_mssql = sys.argv[1]
if (connection_string_mssql is None):
    raise Exception("Connection string unspecified")

training_data = DatabaseService.fetch_training_data(connection_string_mssql)

print(training_data)