using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.UseCases.Admins.Commands;
using UrphaCapital.Application.UseCases.Admins.Queries;
using UrphaCapital.Application.UseCases.Payments.Commands;
using UrphaCapital.Application.UseCases.Payments.Queries;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseModel> PostPayment([FromBody] CreatePaymentCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);

            return response;
        }
        [HttpGet("{index}/{count}")]
        public async Task<IEnumerable<Payment>> GetPayments(int index, int count, CancellationToken cancellation)
        {
            var query = new GetAllPaymentsQuery()
            {
                Index = index,
                Count = count
            };

            var response = await _mediator.Send(query, cancellation);

            return response;
        }
    }
}
