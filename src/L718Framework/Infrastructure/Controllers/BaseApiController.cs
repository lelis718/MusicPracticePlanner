
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace L718Framework.Infrastructure.Controllers;

/// <summary>
/// A base controller for main API that will use the 
/// MediatR
/// </summary>
[ApiController]
[Route("[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ?? HttpContext.RequestServices.GetRequiredService<ISender>();
}
