using Auth.Application.Features.Permissions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebAPI.Controllers
{
    [Route("permissions")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("check")]
        public async Task<IActionResult> Check([FromQuery]CheckPermissionQuery checkPermissionQuery)
        {
            var allowed = await _mediator.Send(checkPermissionQuery);
            return Ok(allowed);
        }
    }
}
