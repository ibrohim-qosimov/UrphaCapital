namespace UrphaCapital.Application.ExternalServices.ErrorSender
{
    public interface IErrorSenderService
    {
        public Task SendError(string message, CancellationToken cancellationToken = default);
    }
}
