using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MbdcLocalBudgetsPresentation.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public abstract class BaseRestApiController : ControllerBase
{
    protected readonly IMapper Mapper;

    protected BaseRestApiController(IMapper mapper)
    {
        Mapper = mapper;
    }
}