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

        [HttpPost]
        public async Task<IActionResult> PreparePaymentAsync(decimal amout)
        {
            var result = await _paymentService.PreparePaymentAsync(amout, "test");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPaymentAsync(string transactionId)
        {
            var result = await _paymentService.ConfirmPaymentAsync(transactionId);

            return Ok(result);
        }
    }
}
