using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("7188316095:AAECrzMzGiqOY3uhQSiTwyoQWBGxBL1f46k");

using CancellationTokenSource cts = new ();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new ()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    string messageText = "";
    if (message.Text is not null)
        messageText = message.Text;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
    if (update.Message.Photo is not null)
    {
        var fileId = update.Message.Photo.Last().FileId;
        if (fileId is not null)
        {
            var fileInfo = await botClient.GetFileAsync(fileId);
            var filePath = fileInfo.FilePath.Substring(7);
            string destinationFilePath = "../../../../" + filePath;

            await using Stream fileStream = System.IO.File.Create(destinationFilePath);
            var file = await botClient.GetInfoAndDownloadFileAsync(
                fileId: fileId,
                destination: fileStream,
                cancellationToken: cancellationToken);
        }
    }
    /*
    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Trying *all the parameters* of `sendMessage` method",
        parseMode: ParseMode.MarkdownV2,
        disableNotification: true,
        replyToMessageId: update.Message.MessageId,
        replyMarkup: new InlineKeyboardMarkup(
            InlineKeyboardButton.WithUrl(
                text: "Check sendMessage method",
                url: "https://core.telegram.org/bots/api#sendmessage")),
        cancellationToken: cancellationToken);
    Console.WriteLine(
        $"{sentMessage.From.FirstName} sent message {sentMessage.MessageId} " +
        $"to chat {sentMessage.Chat.Id} at {sentMessage.Date}. " +
        $"It is a reply to message {sentMessage.ReplyToMessage.MessageId} " +
        $"and has {sentMessage.Entities.Length} message entities.");
    */
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
