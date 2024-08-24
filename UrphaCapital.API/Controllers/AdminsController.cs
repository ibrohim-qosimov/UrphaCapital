using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.AuthServices;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Application.UseCases.Admins.Commands;
using UrphaCapital.Application.UseCases.Admins.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IPasswordHasher _passwordHasher;

        public AdminsController(IMediator mediator, IAuthService authService, IPasswordHasher passwordHasher)
        {
            _mediator = mediator;
            _authService = authService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<ResponseModel> PostAdmin(CreateAdminCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("{id}")]
        public async Task<Admin> GetAdminById(long id, CancellationToken cancellation)
        {
            var query = new GetAdminByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet]
        [EnableRateLimiting("sliding")]
        public async Task<IEnumerable<Admin>> GetAdmins(CancellationToken cancellation)
        {
            var query = new GetAllAdminsQuery();

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> PutAdmin(UpdateAdminCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> RemoveAdmin(long id, CancellationToken cancellation)
        {
            var command = new DeleteAdminCommand { Id = id };

            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpPost("Login")]
        [EnableRateLimiting("sliding")]
        public async Task<string> Login(AdminLogin loginModel, CancellationToken cancellation)
        {
            if (ModelState.IsValid == false)
            {
                throw new InvalidOperationException();
            }

            var query = new GetAdminByEmailQuery()
            {
                Email = loginModel.Email,
            };

            var admin = await _mediator.Send(query, cancellation);

            if (admin == null)
            {
                throw new InvalidOperationException();
            }

            var isPasswordTrue = _passwordHasher.Verify(admin.PasswordHash, loginModel.Password, admin.Salt);

            if (!isPasswordTrue)
            {
                throw new InvalidOperationException("Password is incorrect");
            }

            var token = _authService.GenerateToken(admin);

            return token;
        }
    }
}
