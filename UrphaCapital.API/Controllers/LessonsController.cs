using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Application.UseCases.Lessons.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseModel> PostLesson(CreateLessonCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("GetLessonById/{id}")]
        public async Task<Lesson> GetLessonById(long id, CancellationToken cancellation)
        {
            var query = new GetLessonByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("GetLessonsByCourseId/{courseId}")]
        public async Task<IEnumerable<Lesson>> GetLessonsByCourseId(long courseId, CancellationToken cancellation)
        {
            var query = new GetAllLessonsByCourseIdQuery()
            {
                CourseId = courseId
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> PutLesson(UpdateLessonCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> RemoveLesson(long id, CancellationToken cancellation)
        {
            var command = new DeleteLessonCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }
    }
}
