using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Results.Commands;
using UrphaCapital.Application.UseCases.Results.Queries;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResultsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostResult([FromForm] CreateResultCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutResult([FromForm] UpdateResultCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(long id)
        {
            var command = new DeleteResultCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{size}")]
        public async Task<IActionResult> GetResults(short size)
        {
            var query = new GetResultsQuery { Size = size };

            var result = await _mediator.Send(query);

            return Ok(result);

        }
    }
}
