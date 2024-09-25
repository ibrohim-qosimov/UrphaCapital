using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.ExternalServices.AuthServices;
using UrphaCapital.Application.ExternalServices.HasherServices;
using UrphaCapital.Application.UseCases.Admins.Queries;
using UrphaCapital.Application.UseCases.Mentors.Queries;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Application.ViewModels.AuthModels;

namespace UrphaCapital.API.Controllers
{
    [Route("api/autContoller")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthService _authService;

        public AuthController(IMediator mediator, IPasswordHasher passwordHasher)
        {
            _mediator = mediator;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        [EnableRateLimiting("sliding")]
        public async Task<TokenModel> Login([FromBody] LoginModel loginModel, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }

            // Check for student
            var studentQuery = new GetStudentByEmailQuery { Email = loginModel.Email };
            var student = await _mediator.Send(studentQuery, cancellation);
            if (student != null)
            {
                if (!_passwordHasher.Verify(student.PasswordHash, loginModel.Password, student.Salt))
                {
                    throw new InvalidOperationException("Password is incorrect");
                }

                return _authService.GenerateToken(student);
            }

            //check for mentor
            var mentorQuery = new GetAMentorByEmailQuery { Email = loginModel.Email };
            var mentor = await _mediator.Send(mentorQuery, cancellation);
            if (mentor != null)
            {
                if (!_passwordHasher.Verify(mentor.PasswordHash, loginModel.Password, mentor.Salt))
                {
                    throw new InvalidOperationException("Password is incorrect");
                }

                return _authService.GenerateToken(mentor);
            }
            //check for admin
            var adminQuery = new GetAdminByEmailQuery { Email = loginModel.Email };
            var admin = await _mediator.Send(adminQuery, cancellation);
            if (admin != null)
            {
                if (!_passwordHasher.Verify(admin.PasswordHash, loginModel.Password, admin.Salt))
                {
                    throw new InvalidOperationException("Password is incorrect");
                }

                return _authService.GenerateToken(admin);
            }

            throw new InvalidOperationException("User not found");
        }

    }
}
