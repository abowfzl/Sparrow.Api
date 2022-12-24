using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Sparrow.API.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}