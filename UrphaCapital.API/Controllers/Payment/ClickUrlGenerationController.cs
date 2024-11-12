using Microsoft.AspNetCore.Mvc;
using System.Text;
using UrphaCapital.API.Configurations;

namespace UrphaCapital.API.Controllers.Payment
{
    [Route("api/clickUrlGeneration")]
    [ApiController]
    public class ClickUrlGenerationController(IConfiguration configuration) : ControllerBase
    {
        private readonly ClickConfig _clickConfig = configuration.GetSection("ClickConfig").Get<ClickConfig>()!;

        [HttpGet("generate-click-link")]
        public async Task<IActionResult> GenereteClickUrl(long studentId, int orderId, decimal amount)
        {
            var clickBaseUrl = "https://my.click.uz/services/pay";
            var returnUrl = "https://www.urphacapital.uz/courses";

            StringBuilder clickUrl = new StringBuilder(clickBaseUrl);
            clickUrl.Append("?service_id=" + _clickConfig.ServiceId);
            clickUrl.Append("&merchant_id=" + _clickConfig.MerchantId);
            clickUrl.Append("&amount=" + amount);
            clickUrl.Append("&transaction_param=" + orderId + ":" + studentId);
            clickUrl.Append("&return_url=" + returnUrl);

            return Ok(clickUrl.ToString());
        }

    }
}
