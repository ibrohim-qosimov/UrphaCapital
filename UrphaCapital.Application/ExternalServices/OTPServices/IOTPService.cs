using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.ExternalServices.OTPServices
{
    public interface IOTPService
    {
        public Task<ResponseModel> GenerateAndSendOtpAsync(string email);
        public bool ValidateOtp(string email, string otp);


    }
}
