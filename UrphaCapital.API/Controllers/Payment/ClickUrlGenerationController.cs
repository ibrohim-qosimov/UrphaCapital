using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace UrphaCapital.API.Controllers.Payment
{
    [Route("api/clickUrlGeneration")]
    [ApiController]
    public class ClickUrlGenerationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GenerateUrl(int orderId, decimal amout)
        {
            var serviceId = 37106;
            var merchantId = 28080;
            var clickUrlBase = "https://my.click.uz/services/pay";
            var returnUrl = "https://www.urphacapital.uz/courses";

            StringBuilder clickUrl = new StringBuilder(clickUrlBase);
            clickUrl.Append("?service_id=" + serviceId);
            clickUrl.Append("&merchant_id=" + merchantId);
            clickUrl.Append("&amount=" + amout);
            clickUrl.Append("&transaction_param=" + orderId);
            clickUrl.Append("&return_url=" + returnUrl);

            return Ok(clickUrl.ToString());
        }
    }
}
