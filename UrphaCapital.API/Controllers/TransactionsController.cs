using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.ClickTransactions.Queries;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] long pageIndex, long totalCount)
        {
            var result = await _mediator.Send(new GetAllTransactionsQuery()
            {
                PageIndex = pageIndex,
                TotalCount = totalCount
            });
            return Ok(result);
        }
    }
}
