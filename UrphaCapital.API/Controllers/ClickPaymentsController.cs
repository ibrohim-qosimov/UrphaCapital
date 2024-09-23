using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.PaymentProcessing;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClickPaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public ClickPaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
    }
}
