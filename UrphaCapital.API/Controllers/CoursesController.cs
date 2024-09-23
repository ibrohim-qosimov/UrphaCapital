using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Courses.Commands;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> Create([FromForm] CreateCourseCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("GetById/{id}")]
        public async Task<Course> GetById(string id, CancellationToken cancellation)
        {
            var query = new GetCourseByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("GetAllByMentorId/{mentorId}/{index}/{count}")]
        public async Task<IEnumerable<Course>> GetAllByMentorId(long mentorId, int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllCoursesByMentorIdQuery()
            {
                MentorId = mentorId,
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("{index}/{count}")]
        public async Task<IEnumerable<Course>> GetAll(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllCoursesQuery()
            {
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> Update([FromForm] UpdateCourseCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> Delete(Guid id, CancellationToken cancellation)
        {
            var command = new DeleteCourseCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }
    }
}
