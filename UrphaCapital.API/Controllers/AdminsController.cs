using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UrphaCapital.Application.ExternalServices.AuthServices;
using UrphaCapital.Application.ExternalServices.HasherServices;
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
        public async Task<ResponseModel> PostAdmin([FromBody] CreateAdminCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }

        [HttpGet("{id}")]
        [EnableRateLimiting(policyName: "sliding")]
        public async Task<Admin> GetAdminById(long id, CancellationToken cancellation)
        {
            var query = new GetAdminByIdQuery { Id = id };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpGet("{index}/{count}")]
        [EnableRateLimiting("sliding")]
        //[Authorize("Admin")]
        public async Task<IEnumerable<Admin>> GetAdmins(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllAdminsQuery()
            {
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }

        [HttpPut]
        public async Task<ResponseModel> PutAdmin([FromBody] UpdateAdminCommand command, CancellationToken cancellation)
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
    }
}
