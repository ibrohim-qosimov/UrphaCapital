using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.AuthServices;
using UrphaCapital.Application.UseCases.Admins.Commands;
using UrphaCapital.Application.UseCases.Admins.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public AdminsController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
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
        public async Task<string> Login(AdminLogin loginModel)
        {
        }
    }
}
