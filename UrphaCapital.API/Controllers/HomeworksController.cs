using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Homework.Commands;
using UrphaCapital.Application.UseCases.Homework.Queries;
using UrphaCapital.Application.UseCases.Lessons.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.DTOs;
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

        [HttpPut("grade-homework")]
        public async Task<ResponseModel> PutHomework([FromBody] GradeHomeworkCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("{studentId}/results/{index}/{count}")]
        public async Task<IEnumerable<HomeworkResultView>> GetStudentHomeworkResults(long studentId, int index, int count, CancellationToken cancellation)
        {
            var query = new GetStudentHomeworkResultsQuery()
            {
                StudentId = studentId,
                Index = index,
                Count = count,
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("{index}/{count}")]
        public async Task<IEnumerable<Homeworks>> GetAll(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllHomeworksQuery()
            {
                Index = index,
                Count = count,
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("{mentorId}/{index}/{count}")]
        public async Task<IEnumerable<Homeworks>> GetAllHomeworksByMentorId(int index, int count, long mentorId, CancellationToken cancellation)
        {
            var query = new Application.UseCases.Homework.Queries.GetAllHomeworksByMentorIdQuery()
            {
                MentorId = mentorId,
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("bylesson/{lessonId}/{index}/{count}")]
        public async Task<IEnumerable<Homeworks>> GetAllHomeworksByLessonId(int index, int count, long lessonId, CancellationToken cancellation)
        {
            var query = new Application.UseCases.Homework.Queries.GetAllHomeworksByLessonIdQuery()
            {
                LessonId = lessonId,
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

    }
}
