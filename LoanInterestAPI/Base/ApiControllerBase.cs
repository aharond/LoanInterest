using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase {
    private readonly ISender? _mediator;

    protected ISender Mediator => _mediator ?? HttpContext.RequestServices.GetRequiredService<ISender>();
}