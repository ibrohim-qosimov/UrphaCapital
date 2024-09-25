using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.ExternalServices.AuthServices;
using UrphaCapital.Application.ExternalServices.HasherServices;
using UrphaCapital.Application.UseCases.Mentors.Commands;
using UrphaCapital.Application.UseCases.Mentors.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IPasswordHasher _passwordHasher;

        public MentorsController(IMediator mediator, IPasswordHasher passwordHasher, IAuthService authService)
        {
            _mediator = mediator;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> Create([FromForm] CreateMentorCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<Mentor> GetById(long id, CancellationToken cancellation)
        {
            var query = new GetMentorByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("{index}/{count}")]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<Mentor>> GetAll(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllMentorsQuery()
            {
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> Update([FromForm] UpdateMentorCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseModel> Delete(long id, CancellationToken cancellation)
        {
            var command = new DeleteMentorCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpPost("Login")]
        [EnableRateLimiting("sliding")]
        public async Task<TokenModel> Login([FromBody] MentorLogin loginModel, CancellationToken cancellation)
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
