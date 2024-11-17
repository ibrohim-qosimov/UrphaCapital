using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.UseCases.Help_.Commands;
using UrphaCapital.Application.UseCases.Help_.Queries;
using UrphaCapital.Application.UseCases.Homework.Commands;
using UrphaCapital.Application.UseCases.Homework.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HelpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseModel> PostHelp([FromBody] CreateHelpCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ResponseModel> RemoveHelp(long id, CancellationToken cancellation)
        {
            var command = new DeleteHelpCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [EnableRateLimiting(policyName: "sliding")]
        public async Task<IEnumerable<Help>> GetAll(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllHelpsQuery();

            var response = await _mediator.Send(query, cancellation);

            return response;
        }
    }
}
