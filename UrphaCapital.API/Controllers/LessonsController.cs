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
        public async Task<ResponseModel> PostLesson([FromForm] CreateLessonCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("{id}")]
        public async Task<Lesson> GetLessonById(long id, CancellationToken cancellation)
        {
            var query = new GetLessonByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("getvideo")]
        public async Task<IActionResult> GetLessonVideo([FromQuery] long lessonId, CancellationToken cancellation)
        {
            var query = new GetLessonVideoQuery { Id = lessonId };

            var videoStream = await _mediator.Send(query, cancellation);

            if (videoStream is null)
            {
                return NotFound();
            }

            return new FileStreamResult(videoStream, "video/mp4")
            {
                EnableRangeProcessing = true,
                FileDownloadName = null
            };
        }

        [HttpGet("{courseId}/{index}/{count}")]
        public async Task<IEnumerable<Lesson>> GetLessonsByCourseId(int index, int count, long courseId, CancellationToken cancellation)
        {
            var query = new GetAllLessonsByCourseIdQuery()
            {
                CourseId = courseId,
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> PutLesson([FromForm] UpdateLessonCommand command, CancellationToken cancellation)
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
