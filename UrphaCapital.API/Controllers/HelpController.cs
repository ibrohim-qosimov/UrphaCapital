using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ResponseModel> PostHelp([FromForm] CreateHomeworkCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> RemoveHelp(long id, CancellationToken cancellation)
        {
            var command = new DeleteHomeworkCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<Homeworks>> GetAll(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllHomeworksQuery();

            var response = await _mediator.Send(query, cancellation);

            return response;
        }
    }
}
