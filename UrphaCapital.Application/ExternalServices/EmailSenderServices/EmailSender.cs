using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.ExternalServices.EmailSenderServices
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task<ResponseModel> SendMessageAsync(EmailMessageModel model)
        {
            try
            {
                var emailSettings = _config.GetSection("EmailSettings");
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]),
                    Subject = model.Subject,
                    Body = model.Message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(model.Email);

                using var smtpClient = new SmtpClient(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]))
                {
                    Port = Convert.ToInt32(emailSettings["MailPort"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
                    EnableSsl = true,
                };

                //smtpClient.UseDefaultCredentials = true;

                await smtpClient.SendMailAsync(mailMessage);

                return new ResponseModel()
                {
                    IsSuccess = true,
                    Message = "Email successfully sent.",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
        }
    }
}
