using Auth.Application.Features.Users.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Auth.Application.Features.Authorization.Commands;
using Auth.WebAPI.DTOs;

namespace Auth.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var response = await _mediator.Send(loginCommand);
            return response.ToApiResponse(this);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return response.ToApiResponse(this);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return response.ToApiResponse(this);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return response.ToApiResponse(this);
        }
    }
}
