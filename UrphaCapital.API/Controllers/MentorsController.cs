using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.UseCases.Mentors.Commands;
using UrphaCapital.Application.UseCases.Mentors.Queries;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MentorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseModel> Create(CreateMentorCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("{id}")]
        public async Task<Mentor> GetById(long id, CancellationToken cancellation)
        {
            var query = new GetMentorByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<Mentor>> GetAll(CancellationToken cancellation)
        {
            var query = new GetAllMentorsQuery();

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> Update(UpdateMentorCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(long id, CancellationToken cancellation)
        {
            var command = new DeleteMentorCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpPost("Login")]
        [EnableRateLimiting("sliding")]
        public async Task<string> Login(MentorLogin loginModel, CancellationToken cancellation)
        {
            if (ModelState.IsValid == false)
            {
                throw new InvalidOperationException();
            }

            var query = new GetAMentorByEmailQuery()
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
