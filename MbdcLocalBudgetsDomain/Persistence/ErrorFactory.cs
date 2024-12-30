namespace MbdcLocalBudgetsDomain.Persistence;

public static class ErrorFactory
{
    public static Error NotFoundById<TEntity>(string id) => new Error(
        code: "Error.EntityNotFoundById",
        message: $"ID (${typeof(TEntity).Name}) : {id}"
    );

    public static Error ExcelIsEmpty() => new(
        code: "Error.ExcelIsEmpty",
        message: "Report file has no worksheets!"
    );

    public static Error BudgetReportAlreadyExists(int year, string city) => new(
        code: "Error.BudgetReportAlreadyExists",
        message: $"Report {year} {city} already exist"
    );

    public static Error LocalityNotFound() => new(
        code: "Error.LocalityNotFound",
        message: "Locality was not found");
}