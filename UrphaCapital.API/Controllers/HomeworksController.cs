using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Homework.Commands;
using UrphaCapital.Application.UseCases.Homework.Queries;
using UrphaCapital.Application.UseCases.Lessons.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeworksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ResponseModel> PostLesson([FromForm] CreateHomeworkCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }
        [HttpDelete("{id}")]
        public async Task<ResponseModel> RemoveHomework(long id, CancellationToken cancellation)
        {
            var command = new DeleteHomeworkCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }
        [HttpPut]
        public async Task<ResponseModel> PutHomework([FromForm] UpdateHomeworkCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }
        [HttpGet]
        public async Task<IEnumerable<Homeworks>> GetAll(CancellationToken cancellation)
        {
            var query = new GetAllHomeworksQuery();

            var response = await _mediator.Send(query, cancellation);

            return response;
        }
        [HttpGet("{mentorId}")]
        public async Task<IEnumerable<Homeworks>> GetAllHomeworksByMentorId(long mentorId, CancellationToken cancellation)
        {
            var query = new Application.UseCases.Homework.Queries.GetAllHomeworksByMentorIdQuery()
            {
                MentorId = mentorId
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

    }
}
