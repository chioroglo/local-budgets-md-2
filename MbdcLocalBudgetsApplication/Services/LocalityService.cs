using DnsClient.Protocol;
using MbdcLocalBudgetsDomain.Dto;
using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Persistence;
using MbdcLocalBudgetsDomain.Repositories;
using MbdcLocalBudgetsDomain.Services;
using OfficeOpenXml;
using System.Linq;

namespace MbdcLocalBudgetsApplication.Services;

public class LocalityService : ILocalityService
{
    private readonly ILocalityRepository _localityRepository;
    private readonly IDistrictRepository _districtRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LocalityService(ILocalityRepository localityRepository,
        IDistrictRepository districtRepository,
        IUnitOfWork unitOfWork)
    {
        _localityRepository = localityRepository;
        _districtRepository = districtRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Locality?>> GetById(Guid id, CancellationToken ct = default)
    {
        var locality = await _localityRepository.GetById(id, ct);

        if (locality == null)
        {
            return Result.Failure<Locality>(
                ErrorFactory.LocalityNotFound()
            );
        }

        return Result.Success(locality);
    }

    public async Task<Result> Upload(MemoryStream file, CancellationToken ct = default)
    {
        using var package = new ExcelPackage(file);
        using var worksheet = package.Workbook.Worksheets.FirstOrDefault();

        if (worksheet == null)
        {
            var error = ErrorFactory.ExcelIsEmpty();
            return Result.Failure(error);
        }

        var existingDistricts = await _districtRepository.GetAll(ct);
        var existingLocalities = await _localityRepository.GetAll(ct);

        var parsedRows = ParseExcelRows(worksheet);

        var districtsWithLocalities = ExtractDistrictsWithLocalities(parsedRows);
        districtsWithLocalities = districtsWithLocalities.Where(dwl =>
                !existingDistricts.Select(ed => ed.Code).Contains(dwl.Code) &&
                !existingLocalities.Select(el => el.Code).Contains(dwl.Code)
                );
        await _districtRepository.Add(districtsWithLocalities, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    private static IEnumerable<ExcelLocalityDto> ParseExcelRows(ExcelWorksheet worksheet)
    {
        for (var row = 2; row < worksheet.Dimension.Rows; row++)
        {
            var code = worksheet.Cells[row, 1].Text?.Trim();
            var parentCode = worksheet.Cells[row, 2].Text?.Trim();
            var nameRo = worksheet.Cells[row, 3].Text?.Trim();
            var status = worksheet.Cells[row, 7].Text?.Trim();

            if (code == null || nameRo == null || status == null)
            {
                continue;
            }

            var record = new ExcelLocalityDto
            {
                Code = code,
                ParentCode = parentCode,
                Status = status,
                Name = nameRo
            };
            yield return record;
        }
    }

    private static IEnumerable<District> ExtractDistrictsWithLocalities(IEnumerable<ExcelLocalityDto> parsed)
    {
        return parsed
            .Where(l => string.IsNullOrEmpty(l.ParentCode))
            .Select(l => new District
            {
                Name = l.Name,
                Code = l.Code,
                Status = l.GetParsedStatus(),
                Localities = parsed
                    .Where(l1 => string.Equals(l1.ParentCode, l.Code, StringComparison.Ordinal))
                    .Select(l1 => new Locality
                {
                    Name = l1.Name,
                    Code = l1.Code,
                    Status = l1.GetParsedStatus(),
                    Population = null
                }).ToList()
            });
    }
}