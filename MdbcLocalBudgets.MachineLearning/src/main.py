import sys
from budget_machine_learning import BudgetMachineLearning
from database_service import DatabaseService
from utils import ColorfulPrint


if __name__ != "__main__":
    exit()

connection_string_mssql = sys.argv[1]
if (connection_string_mssql is None):
    raise Exception("Connection string unspecified")

# step 1 fetch data
ColorfulPrint.print_success("Fetching training data from MS SQL view")

training_data = DatabaseService.fetch_training_data(connection_string_mssql)

ColorfulPrint.print_success("Data acquired")

# step 2 preprocess data
ColorfulPrint.print_success("preprocessing data")

algorithm = BudgetMachineLearning()
X, y = algorithm.preprocess_data(training_data)
print(X)
print(y)
categorical_features = ["DistrictCode", "LocalityCode"]

ColorfulPrint.print_success("Data preprocessing completed")

# step 3 split data to training/test groups

ColorfulPrint.print_success("Splitting data to training/test groups")

X_train, X_test, y_train, y_test = algorithm.split_data(X, y)

ColorfulPrint.print_success("Data split complete")

# step 4 Train CatBoost

ColorfulPrint.print_success("Training CatBoost")

models = algorithm.train_catboost(X_train, y_train, cat_features= categorical_features)

ColorfulPrint.print_success("Trained CatBoosts")

# step 5 Evaluate CatBoosts

print(models)

for target, model in models.items():
    algorithm.evaluate_model(target, model, X_test, y_test)