using DataProcessing;
using System.Numerics;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class TGBot
    {
        private TelegramBotClient? botClient = null;
        private List<ChatData> chatsData = new List<ChatData>();

        public TGBot()
        {
            botClient = new TelegramBotClient("7188316095:AAECrzMzGiqOY3uhQSiTwyoQWBGxBL1f46k");
            using CancellationTokenSource cts = new();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>(),
                ThrowPendingUpdates = true
            };
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long chatId = 0;
            ChatData? curChat = null;
            List<Plant> plants = new List<Plant>();
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    await TGUpdateCallbackHandler.HandleCallbacksAsync(botClient, update, chatsData, cancellationToken);
                    break;
                case UpdateType.Message:
                    await TGUpdateMessageHandler.HandleMessagesAsync(botClient, update, chatsData, cancellationToken);
                    break;
            }
        }
        
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

             Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}