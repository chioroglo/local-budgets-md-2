using System.ComponentModel.DataAnnotations;
using MapsterMapper;
using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Requests;
using MbdcLocalBudgetsDomain.Services;
using MbdcLocalBudgetsInfrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MbdcLocalBudgetsPresentation.Controllers;

[Route("budget-report")]
public class BudgetReportController : BaseRestApiController
{
    private readonly IAnnualBudgetReportService _annualBudgetReportService;
    public BudgetReportController(IAnnualBudgetReportService annualBudgetReportService, IMapper mapper) : base(mapper)
    {
        _annualBudgetReportService = annualBudgetReportService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddBudgetReportRequest request, CancellationToken ct = default)
    {
        var record = Mapper.Map<AnnualBudgetReport>(request);
        await _annualBudgetReportService.Add(record, ct);
        return Ok();
    }

    [HttpGet("{id:maxlength(100)}")]
    public async Task<IActionResult> GetById([FromRoute] [Required] string id, CancellationToken ct = default)
    {
        var result = await _annualBudgetReportService.GetById(id, ct);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        // TODO format in error JSON object
        return BadRequest(result.Error.Message);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        [Required] IFormFile report,
        [FromQuery] [Required] string city,
        [FromQuery] [Required] int year,
        CancellationToken ct)
    {
        if (report == null || report.Length == 0)
        {
            // todo in JSON error object
            var response = ErrorResponseFactory.BadRequest("Report is empty");
            return BadRequest(response);
        }

        using (var stream = new MemoryStream())
        {
            await report.CopyToAsync(stream, ct);
            stream.Position = 0;
            var result = await _annualBudgetReportService.Upload(stream, year, city, ct);
            if (result.IsFailure)
            {
                var response = ErrorResponseFactory.BadRequest(result.Error.Message);
                return BadRequest(response);
            }
        }

        return Ok();
    }
}