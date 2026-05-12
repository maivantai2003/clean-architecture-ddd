using Chat.Application.Features.Chats.Commands;
using Chat.Application.Features.Chats.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers
{
    [ApiController]
    [Route("api/chats")]
    public sealed class ChatsController : ControllerBase
    {
        private readonly IMediator _mediator;

n        public ChatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChatCommand command)
        {
            var chatId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { chatId }, new { chatId });
        }

        [HttpPost("{chatId:guid}/messages")]
        public async Task<IActionResult> AddMessage(Guid chatId, [FromBody] AddChatMessageCommand command)
        {
            if (chatId != command.ChatId)
            {
                return BadRequest("ChatId in route and body must match.");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{chatId:guid}")]
        public async Task<IActionResult> GetById(Guid chatId)
        {
            var chat = await _mediator.Send(new GetChatByIdQuery { ChatId = chatId });
            return chat == null ? NotFound() : Ok(chat);
        }

        [HttpGet("participant/{participantId:guid}")]
        public async Task<IActionResult> GetByParticipant(Guid participantId)
        {
            var chats = await _mediator.Send(new GetChatsQuery { ParticipantId = participantId });
            return Ok(chats);
        }
    }
}
