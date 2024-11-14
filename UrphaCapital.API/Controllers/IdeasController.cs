using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.UseCases.IdeasCrud.Commands;
using UrphaCapital.Application.UseCases.IdeasCrud.Queries;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdeasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostIdea([FromForm] CreateIdeaCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutIdeas([FromForm] UpdateIdeaCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdea(long id)
        {
            var command = new DeleteIdeaCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [EnableRateLimiting(policyName: "sliding")]
        public async Task<IActionResult> GetIdeas()
        {
            var query = new GetAllIdeasQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
