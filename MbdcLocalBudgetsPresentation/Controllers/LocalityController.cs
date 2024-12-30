using MapsterMapper;
using MbdcLocalBudgetsDomain.Models;
using MbdcLocalBudgetsDomain.Services;
using MbdcLocalBudgetsInfrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MbdcLocalBudgetsPresentation.Controllers;

[Route("locality")]
public class LocalityController : BaseRestApiController
{
    private readonly ILocalityService _localityService;

    public LocalityController(
        IMapper mapper,
        ILocalityService localityService) : base(mapper)
    {
        _localityService = localityService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLocalityById(Guid id, CancellationToken ct)
    {
        var locality = await _localityService.GetById(id, ct);
        var localityModel = Mapper.Map<LocalityModel>(locality);
        return Ok(localityModel);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile report, CancellationToken ct)
    {
        if (report == null || report.Length == 0)
        {
            return BadRequest();
        }
        using (var stream = new MemoryStream())
        {
            await report.CopyToAsync(stream, ct);
            stream.Position = 0;
            var result = await _localityService.Upload(stream, ct);
            if (result.IsFailure)
            {
                var response = ErrorResponseFactory.BadRequest(result.Error.Message);
                return BadRequest(response);
            }
        }

        return Ok();
    }
}