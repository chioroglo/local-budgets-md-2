from catboost import CatBoostRegressor
import pandas
import joblib
from sklearn.metrics import mean_squared_error
from sklearn.model_selection import train_test_split


class BudgetMachineLearning:
    MODEL_TARGETS = ["ActualExpenses", "ActualIncome"]

    def preprocess_data(self, data: pandas.DataFrame) -> tuple[pandas.DataFrame, pandas.DataFrame]:
        data = data[(data != 0).all(axis=1)]

        # split features and targets
        X = data.drop(columns=self.MODEL_TARGETS)
        y = data[self.MODEL_TARGETS]

        return X, y
    
    def split_data(self, X, y):
        return train_test_split(X, y, test_size=0.2, random_state=42)
    
    def train_catboost(self, X_train, y_train, cat_features):
        models = {}
        targets = self.MODEL_TARGETS
        for target in targets:
            print(f"Training model for {target}")
            model = CatBoostRegressor(
                iterations=500,
                learning_rate=0.1,
                depth=6,
                cat_features=cat_features,
                verbose=100
            )
            model.fit(X_train, y_train[target])
            models[target] = model
        return models
    
    def evaluate_model(self, target: str , model: CatBoostRegressor, X_test, y_test):
        predictions = model.predict(X_test)
        mse = mean_squared_error(y_test[target], predictions)
        print(f"\nModel Performance for {target}:")
        print(f"  Mean Squared Error: {mse:.2f}")

    def save_model_on_disk(self, target: str, model: CatBoostRegressor):
        joblib.dump(model, f"catboost_{target}.joblib")
