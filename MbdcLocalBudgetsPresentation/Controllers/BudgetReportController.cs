using MapsterMapper;
using MbdcLocalBudgetsDomain.Entities;
using MbdcLocalBudgetsDomain.Services;
using MbdcLocalBudgetsPresentation.HttpRequests;
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
    public async Task<IActionResult> Add([FromBody] AddBudgetReportHttpRequest request, CancellationToken ct = default)
    {
        var record = Mapper.Map<AnnualBudgetReport>(request);
        await _annualBudgetReportService.Add(record, ct);
        return Ok();
    }

    [HttpGet("{id:maxlength(100)}")]
    public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken ct = default)
    {
        var result = await _annualBudgetReportService.GetById(id, ct);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error.Message);
    }
}