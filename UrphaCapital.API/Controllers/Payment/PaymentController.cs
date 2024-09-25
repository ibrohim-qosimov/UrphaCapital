using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.API.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("prepare")]
        public async Task<IActionResult> Preapare(PrepareRequest prepareRequest)
        {
            return Ok("smtng");
        }

        [HttpPost("complate")]
        public async Task<IActionResult> Complate(ComplateRequest complateRequest)
        {
            return Ok("smtng");
        }
    }
}
