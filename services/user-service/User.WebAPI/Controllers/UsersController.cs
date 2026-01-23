using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Features.Users.Commands;
using User.Application.Features.Users.Queries;
using User.WebAPI.DTOs;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _mediator.Send(new GetAllUserQuery());
            return Ok(response);
        }
        [HttpPost("email")]
        public async Task<IActionResult> CreateUser([FromBody] UserUpdateEmailCommand userUpdateEmail)
        {
            var response = await _mediator.Send(userUpdateEmail);
            return response.ToApiResponse(this);
        }
        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUser(string id)
        {
            var response = await _mediator.Send(new GetUserQuery(Guid.Parse(id)));
            return Ok(response);
        }
    }
}
