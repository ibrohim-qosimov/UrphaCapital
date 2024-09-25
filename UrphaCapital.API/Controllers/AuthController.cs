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
    [Route("api/authContoller")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthService _authService;

        public AuthController(IMediator mediator, IPasswordHasher passwordHasher,IAuthService authService)
        {
            _mediator = mediator;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }
        [HttpPost("login")]
        [EnableRateLimiting("sliding")]
        public async Task<ActionResult<TokenModel>> Login([FromBody] LoginModel loginModel, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid login model.");
            }

            // Check for student
            var studentQuery = new GetStudentByEmailQuery { Email = loginModel.Email };
            var student = await _mediator.Send(studentQuery, cancellation);
            if (student != null)
            {
                if (student.PasswordHash == null || student.Salt == null)
                {
                    return BadRequest("User credentials are not set properly.");
                }

                if (!_passwordHasher.Verify(student.PasswordHash, loginModel.Password, student.Salt))
                {
                    return Unauthorized("Password is incorrect for the student.");
                }

                return Ok(_authService.GenerateToken(student));
            }

            // Check for mentor
            var mentorQuery = new GetAMentorByEmailQuery { Email = loginModel.Email };
            var mentor = await _mediator.Send(mentorQuery, cancellation);
            if (mentor != null)
            {
                if (mentor.PasswordHash == null || mentor.Salt == null)
                {
                    return BadRequest("User credentials are not set properly.");
                }

                if (!_passwordHasher.Verify(mentor.PasswordHash, loginModel.Password, mentor.Salt))
                {
                    return Unauthorized("Password is incorrect for the mentor.");
                }

                return Ok(_authService.GenerateToken(mentor));
            }

            // Check for admin
            var adminQuery = new GetAdminByEmailQuery { Email = loginModel.Email };
            var admin = await _mediator.Send(adminQuery, cancellation);
            if (admin != null)
            {
                if (admin.PasswordHash == null || admin.Salt == null)
                {
                    return BadRequest("User credentials are not set properly.");
                }

                if (!_passwordHasher.Verify(admin.PasswordHash, loginModel.Password, admin.Salt))
                {
                    return Unauthorized("Password is incorrect for the admin.");
                }

                return Ok(_authService.GenerateToken(admin));
            }

            return NotFound("User not found.");
        }

    }
}
