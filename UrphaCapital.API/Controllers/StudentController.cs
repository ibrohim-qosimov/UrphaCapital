using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Application.UseCases.Lessons.Queries;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseModel> PostStudent(CreateStudentsCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<Student> GetStudentById(long id, CancellationToken cancellation)
        {
            var query = new GetAllStudentsByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("GetStudentByCourseId/{courseId}")]
        public async Task<IEnumerable<Student>> GetStudentsByStudentId(long Id, CancellationToken cancellation)
        {
            var query = new GetAllStudentsQuery()
            {
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> PutStudent(UpdateStudentCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> RemoveStudent(long id, CancellationToken cancellation)
        {
            var command = new DeleteLessonCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }
    }
}

