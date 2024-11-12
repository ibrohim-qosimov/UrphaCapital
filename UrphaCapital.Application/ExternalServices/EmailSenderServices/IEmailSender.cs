using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.ExternalServices.EmailSenderServices
{
    public interface IEmailSender
    {
        public Task<ResponseModel> SendMessageAsync(EmailMessageModel model);

    }
}
