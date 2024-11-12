using Microsoft.AspNetCore.Mvc;
using UrphaCapital.Application.ExternalServices.OTPServices;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOTPService _otpService;

        public OtpController(IOTPService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateOtp([FromBody] EmailRequestModel model)
        {
            var response = await _otpService.GenerateAndSendOtpAsync(model.Email);
            if (response.IsSuccess)
                return Ok(response);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("validate")]
        public IActionResult ValidateOtp([FromBody] OtpValidationModel model)
        {
            var isValid = _otpService.ValidateOtp(model.Email, model.Otp);
            if (isValid)
                return Ok(new { IsValid = true, Message = "OTP is valid." });

            return BadRequest(new { IsValid = false, Message = "Invalid or expired OTP." });
        }
    }
}
