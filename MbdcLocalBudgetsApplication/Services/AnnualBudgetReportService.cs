using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Helpers;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsDomain.Repositories;
using MbdcLocalBudgetsDomain.Services;
using OfficeOpenXml;

namespace MbdcLocalBudgetsApplication.Services;

public class AnnualBudgetReportService : IAnnualBudgetReportService
{
    private readonly IAnnualBudgetReportRepository _annualBudgetReportRepository;
    private readonly ICityRepository _cityRepository;

    public AnnualBudgetReportService(IAnnualBudgetReportRepository annualBudgetReportRepository, ICityRepository cityRepository)
    {
        _annualBudgetReportRepository = annualBudgetReportRepository;
        _cityRepository = cityRepository;
    }

    public async Task<Result> Upload(MemoryStream report, int year, string city, CancellationToken ct = default)
    {
        // Make first char of city name start from uppercase letter
        city = CapitalizeFirstCharacter(city);

        if (await _annualBudgetReportRepository.ReportAlreadyExists(year, city, ct))
        {
            var error = ErrorFactory.BudgetReportAlreadyExists(year, city);
            return Result.Failure(error);
        }

        using var package = new ExcelPackage(report);
        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

        if (worksheet == null)
        {
            var error = ErrorFactory.ExcelIsEmpty();
            return Result.Failure(error);
        }

        if (await _cityRepository.DoesNotExist(city, ct))
        {
            await _cityRepository.Add(new City { Name = city }, ct);
        }

        var record = new AnnualBudgetReport
        {
            Year = year,
            City = city,
        };
        var filledCategories = new HashSet<string>();

        for (var row = 1; row <= worksheet.Dimension.Rows; row++)
        {
            var label = worksheet.Cells[row, 1].Text;
            if (label == null)
            {
                continue;
            }
            label = label.Trim();

            foreach (var category in BudgetReportParsingHelper.Categories)
            {
                var parsedValue = worksheet.Cells[row, 2].Text;

                if (string.IsNullOrWhiteSpace(parsedValue) ||
                    filledCategories.Contains(category.Key) ||
                    !label.Contains(category.Key, StringComparison.Ordinal))
                {
                    continue;
                }

                long.TryParse(parsedValue.Replace(" ", ""), out var value);

                // Perform action that fills respective field of an object
                category.Value(record, value);
                filledCategories.Add(category.Key);
                break;
            }
        }

        await _annualBudgetReportRepository.Add(record, ct);
        return Result.Success();
    }

    public async Task<Result<AnnualBudgetReport>> GetById(string id, CancellationToken ct = default)
    {
        var result = await _annualBudgetReportRepository.GetById(id, ct);

        if (result == null)
        {
            return Result.Failure<AnnualBudgetReport>(
                ErrorFactory.NotFoundById<AnnualBudgetReport>(id));
        }
        return Result.Success(result);
    }

    public async Task Add(AnnualBudgetReport record, CancellationToken ct = default)
    {
        await _annualBudgetReportRepository.Add(record, ct);
    }

    private static string CapitalizeFirstCharacter(string value)
    {
        return string.Concat(value[0].ToString().ToUpper(), value.AsSpan(1));
    }
}