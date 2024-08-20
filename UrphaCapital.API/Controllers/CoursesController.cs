using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<ResponseModel> Create(CreateCourseCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("GetById/{id}")]
        public async Task<Course> GetById(long id, CancellationToken cancellation)
        {
            var query = new GetCourseByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("GetAllByMentorId/{mentorId}")]
        public async Task<IEnumerable<Course>> GetAllByMentorId(long mentorId, CancellationToken cancellation)
        {
            var query = new GetAllCoursesByMentorIdQuery()
            {
                MentorId = mentorId
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> GetAll(CancellationToken cancellation)
        {
            var query = new GetAllCoursesQuery();

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> Update(UpdateCourseCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(long id, CancellationToken cancellation)
        {
            var command = new DeleteCourseCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }
    }
}
