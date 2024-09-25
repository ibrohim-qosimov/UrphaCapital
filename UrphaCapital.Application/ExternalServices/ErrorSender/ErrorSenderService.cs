using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace UrphaCapital.Application.ExternalServices.ErrorSender
{
    public class ErrorSenderService : IErrorSenderService
    {
        private readonly TelegramBotClient telegramBotClient;
        private readonly IConfiguration _configuration;

        public ErrorSenderService(TelegramBotClient telegramBotClient, IConfiguration configuration)
        {
            this.telegramBotClient = telegramBotClient;
            _configuration = configuration;
        }

        public async Task SendError(string message, CancellationToken cancellationToken = default)
        {
            await telegramBotClient.SendTextMessageAsync(chatId: $"{_configuration.GetSection("TelegramBot").GetSection("ChatId").Value}",
                text: $"{message}",
                cancellationToken: cancellationToken);
        }
    }
}
