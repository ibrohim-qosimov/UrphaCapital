using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.AuthServices;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IPasswordHasher _passwordHasher;

        public StudentController(IMediator mediator, IAuthService authService, IPasswordHasher passwordHasher)
        {
            _mediator = mediator;
            _authService = authService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<ResponseModel> PostStudent([FromBody] CreateStudentsCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudentById(long id, CancellationToken cancellation)
        {
            var query = new GetAllStudentsByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("{index}/{count}")]
        public async Task<IEnumerable<Student>> GetStudentsByStudentId(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllStudentsQuery()
            {
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> PutStudent([FromBody] UpdateStudentCommand command, CancellationToken cancellation)
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

        [HttpPost("Login")]
        [EnableRateLimiting("sliding")]
        public async Task<string> Login([FromBody] AdminLogin loginModel, CancellationToken cancellation)
        {
            if (ModelState.IsValid == false)
            {
                throw new InvalidOperationException();
            }

            var query = new GetStudentByEmailQuery()
            {
                Email = loginModel.Email,
            };

            var student = await _mediator.Send(query, cancellation);

            if (student == null)
            {
                throw new InvalidOperationException();
            }

            var isPasswordTrue = _passwordHasher.Verify(student.PasswordHash, loginModel.Password, student.Salt);

            if (!isPasswordTrue)
            {
                throw new InvalidOperationException("Password is incorrect");
            }

            var token = _authService.GenerateToken(student);

            return token;
        }
    }
}

