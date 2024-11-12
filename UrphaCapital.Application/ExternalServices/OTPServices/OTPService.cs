using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ExternalServices.EmailSenderServices;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.ExternalServices.OTPServices
{
    public class OTPService : IOTPService
    {
        private readonly IMemoryCache _cache;
        private readonly IEmailSender _emailSender;
        private const string webSite = "https://urphacapital.uz";

        public OTPService(IMemoryCache cache, IEmailSender emailSender)
        {
            _cache = cache;
            _emailSender = emailSender;
        }

        public async Task<ResponseModel> GenerateAndSendOtpAsync(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            var cacheKey = $"OTP_{email}";
            _cache.Set(cacheKey, otp, TimeSpan.FromMinutes(5));

            var emailMessage = new EmailMessageModel
            {
                Email = email,
                Subject = "Your OTP Code",
                Message = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>UrphaCapital OTP Verification</title>\r\n    <style>\r\n        body {{\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f4f4f9;\r\n            color: #333;\r\n            margin: 0;\r\n            padding: 0;\r\n        }}\r\n        .container {{\r\n            max-width: 600px;\r\n            margin: 30px auto;\r\n            background-color: #ffffff;\r\n            padding: 20px;\r\n            border-radius: 8px;\r\n            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);\r\n            text-align: center;\r\n        }}\r\n        .logo {{\r\n            font-size: 24px;\r\n            font-weight: bold;\r\n            color: #4CAF50;\r\n            margin-bottom: 20px;\r\n        }}\r\n        .otp-box {{\r\n            font-size: 24px;\r\n            font-weight: bold;\r\n            color: #4CAF50;\r\n            margin: 20px 0;\r\n            letter-spacing: 4px;\r\n        }}\r\n        .message {{\r\n            font-size: 16px;\r\n            line-height: 1.6;\r\n            color: #555;\r\n        }}\r\n        .footer {{\r\n            font-size: 12px;\r\n            color: #aaa;\r\n            margin-top: 20px;\r\n        }}\r\n        .button {{\r\n            display: inline-block;\r\n            margin-top: 20px;\r\n            padding: 10px 20px;\r\n            background-color: #4CAF50;\r\n            color: #fff;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n            font-weight: bold;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <div class=\"logo\">UrphaCapital</div>\r\n        <p class=\"message\">Hello,</p>\r\n        <p class=\"message\">To complete your verification process, please use the following OTP code:</p>\r\n        <div class=\"otp-box\">{otp}</div>\r\n        <p class=\"message\">This code is valid for 5 minutes. If you didn't request this code, please ignore this email.</p>\r\n        <a href=\"{webSite}\" class=\"button\">Visit UrphaCapital</a>\r\n        <div class=\"footer\">Thank you for choosing UrphaCapital.</div>\r\n    </div>\r\n</body>\r\n</html>\r\n"
            };

            return await _emailSender.SendMessageAsync(emailMessage);
        }

        public bool ValidateOtp(string email, string otp)
        {
            var cacheKey = $"OTP_{email}";
            if (_cache.TryGetValue(cacheKey, out string cachedOtp) && cachedOtp == otp)
            {
                _cache.Remove(cacheKey);
                return true;
            }
            return false;
        }
    }
}
