using MediatR;
using Microsoft.AspNetCore.Mvc;
using Search.Application.Features.Search.Queries;
namespace Search.WebAPI.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int page = 1, int size = 10)
        {
            var response=await _mediator.Send(new SearchUserQuery(keyword, page, size));
            return Ok(response);
        }
    }
}
