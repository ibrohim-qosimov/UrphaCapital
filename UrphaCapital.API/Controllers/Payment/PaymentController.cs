using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.ExternalServices.PaymentProcessing;
using UrphaCapital.Application.ViewModels.PaymentModels;
using UrphaCapital.Application.ViewModels.PaymentModels.Exceptions;

namespace UrphaCapital.API.Controllers.Payment
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("prepare")]
        public async Task<IActionResult> Preapare(PrepareRequest prepareRequest)
        {
            try
            {
                var response = await _paymentService.Prepare(prepareRequest);
                return Ok(response);
            }
            catch (InvalidSignStringException)
            {
                return BadRequest(new { errpr = -1, error_note = "Invalid sign_string" });
            }
        }

        [HttpPost("complate")]
        public async Task<IActionResult> Complate(ComplateRequest complateRequest)
        {
            try
            {
                var response = await _paymentService.Complate(complateRequest);
                return Ok(response);
            }
            catch (InvalidSignStringException)
            {
                return BadRequest(new { errpr = -1, error_note = "Invalid sign_string" });
            }
        }
    }
}
