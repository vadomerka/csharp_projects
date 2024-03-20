using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.UpdateHandlers;

namespace TelegramBot
{
    /// <summary>
    /// Класс бота. Хранит данные чатов.
    /// </summary>
    public class TGBot
    {
        private TelegramBotClient? botClient = null;
        // Список всех чатов.
        private List<ChatData> chatsData = new List<ChatData>();

        public TGBot()
        {
            botClient = new TelegramBotClient("7188316095:AAECrzMzGiqOY3uhQSiTwyoQWBGxBL1f46k");
            using CancellationTokenSource cts = new();
            // Бот не должен реагировать на сообщения, которые он пропустил в выкключенном состоянии.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>(),
                ThrowPendingUpdates = true
            };
            // Запускаем бота.
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        /// <summary>
        /// Главный метод работы с сообщениями.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Информация о сообщении.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                // Обработка Callback update.
                case UpdateType.CallbackQuery:
                    await TGUpdateCallbackHandler.HandleCallbacksAsync(botClient, update, chatsData, cancellationToken);
                    break;
                // Обработка Message update.
                case UpdateType.Message:
                    await TGUpdateMessageHandler.HandleMessagesAsync(botClient, update, chatsData, cancellationToken);
                    break;
            }
        }

        /// <summary>
        /// Метод логирует ошибку в консоль.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Информация о сообщении.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Task.CompletedTask.</returns>
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