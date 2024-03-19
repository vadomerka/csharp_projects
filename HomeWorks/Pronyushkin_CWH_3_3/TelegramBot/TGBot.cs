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
        private ReplyKeyboardMarkup menuKeyboard = new(new[]
        {
            new KeyboardButton[] {
                "Произвести выборку.",
                "Отсортировать данные."
            },
            new KeyboardButton[] {
                "Скачать данные."
            }
        })
        {
            ResizeKeyboard = true
        };

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
                    if (update.CallbackQuery is null || update.CallbackQuery.Message is null) return;
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    curChat = await TGHelper.GetCurChat(chatId, botClient, chatsData, cancellationToken);

                    if (await TGHelper.CurChatCheck(curChat, botClient, chatsData, cancellationToken) 
                        || curChat.Data is null) return;
                    plants = curChat.GetPlants();
                    switch (update.CallbackQuery.Data)
                    {
                        case "sort/straight":
                            plants.Sort((Plant x, Plant y) =>
                            {
                                return x.LatinName.CompareTo(y.LatinName);
                            });
                            await botClient.SendTextMessageAsync(
                            chatId: curChat.Id,
                            text: "Данные отсортированы.",
                            cancellationToken: cancellationToken);
                            break;
                        case "sort/reverse":
                            plants.Sort((Plant x, Plant y) =>
                            {
                                return y.LatinName.CompareTo(x.LatinName);
                            });
                            await botClient.SendTextMessageAsync(
                            chatId: curChat.Id,
                            text: "Данные отсортированы.",
                            cancellationToken: cancellationToken);
                            break;
                        case "fetch/LandscapingZone":
                            curChat.FetchCount = 1;
                            curChat.FetchedValues = new List<string>();
                            curChat.FetchedCols = new string[] { "LandscapingZone", "" };
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "Введите значение по которому нужно отфильтровать данные.",
                                cancellationToken: cancellationToken);
                            break;
                        case "fetch/LocationPlace":
                            curChat.FetchCount = 1;
                            curChat.FetchedValues = new List<string>();
                            curChat.FetchedCols = new string[] { "LocationPlace", "" };
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "Введите значение по которому нужно отфильтровать данные.",
                                cancellationToken: cancellationToken);
                            break;
                        case "fetch/LandscapingZone&ProsperityPeriod":
                            curChat.FetchCount = 2;
                            curChat.FetchedValues = new List<string>();
                            curChat.FetchedCols = new string[] { "LandscapingZone", "ProsperityPeriod" };
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "Введите значение по которому нужно отфильтровать данные.",
                                replyMarkup: null,
                                cancellationToken: cancellationToken);
                            break;
                    }
                    curChat.UpdatePlants(plants);
                    break;
                case UpdateType.Message:
                    if (update.Message is null) return;
                    chatId = update.Message.Chat.Id;
                    curChat = await TGHelper.GetCurChat(chatId, botClient, chatsData, cancellationToken);

                    string messageText = "";
                    if (update.Message.Text is not null)
                        messageText = update.Message.Text;
                    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

                    // Если пользователь прислал файл.
                    if (!await HandleUploadedDocuments(update, cancellationToken, curChat)) return;

                    if (!await HandleFetchingMessages(update, cancellationToken, curChat)) return;

                    // Ответный центр.
                    switch (messageText)
                    {
                        case "Произвести выборку.":
                            if (await TGHelper.CurChatCheck(curChat, botClient, chatsData, cancellationToken) || 
                                curChat.Data is null) return;
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "По какому полю вы хотите провести выборку?",
                                replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
                                    new[] {
                                        InlineKeyboardButton.WithCallbackData("LandscapingZone.", "fetch/LandscapingZone"),
                                        InlineKeyboardButton.WithCallbackData("LocationPlace.", "fetch/LocationPlace"),
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("LandscapingZone + ProsperityPeriod.",
                                        "fetch/LandscapingZone&ProsperityPeriod")
                                    }
                                }),
                                cancellationToken: cancellationToken);
                            return;
                        case "Отсортировать данные.":
                            if (await TGHelper.CurChatCheck(curChat, botClient, chatsData, cancellationToken) || curChat.Data is null) return;
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "В каком порядке вы хотите отсортировать объекты?\nСортировка будет происходить по полю LatinName",
                                replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
                                    new[] {
                                        InlineKeyboardButton.WithCallbackData("В прямом.", "sort/straight"),
                                        InlineKeyboardButton.WithCallbackData("В обратном.", "sort/reverse"),
                                    }}),
                                cancellationToken: cancellationToken);
                            return;
                        case "Скачать данные.":
                            if (await TGHelper.CurChatCheck(curChat, botClient, chatsData, cancellationToken) || curChat.Data is null) return;
                            ReplyKeyboardMarkup formatChoice = new(new[]
                            {
                                new KeyboardButton[] {
                                    "Скачать в формате csv.",
                                    "Скачать в формате json."
                                }
                            })
                            {
                                ResizeKeyboard = true
                            };
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "В каком формате вы хотите скачать данные?",
                                replyMarkup: formatChoice,
                                cancellationToken: cancellationToken);
                            return;
                        case "Скачать в формате csv.":
                            if (await TGHelper.CurChatCheck(curChat, botClient, chatsData, cancellationToken) || curChat.Data is null) return;
                            try
                            {
                                using (Stream fileStream = await cp.WriteAsync(curChat.Data))
                                {
                                    if (fileStream is null) throw new ArgumentNullException();
                                    await TGHelper.SendStreamFile(fileStream, curChat, botClient);
                                }
                            }
                            catch (Exception ex)
                            {
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: $"Произошла ошибка.\n{ex.Message}",
                                    cancellationToken: cancellationToken);
                            }
                            break;
                        case "Скачать в формате json.":
                            if (await TGHelper.CurChatCheck(curChat, botClient, chatsData, cancellationToken) || curChat.Data is null) return;
                            try
                            {
                                plants = curChat.GetPlants();
                                using (Stream fileStream = await jp.WriteAsync(plants))
                                {
                                    if (fileStream is null) throw new ArgumentNullException();
                                    await TGHelper.SendStreamFile(fileStream, curChat, botClient, "plants.json");
                                }
                            }
                            catch (Exception ex)
                            {
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: $"Произошла ошибка.\n{ex.Message}",
                                    cancellationToken: cancellationToken);
                            }
                            break;
                        case "Перезаписать.":
                            if (botClient is null) return;
                            if (curChat.BufferData is null)
                            {
                                await botClient.SendTextMessageAsync(
                                    chatId: curChat.Id,
                                    text: "Нет данных для перезаписи.",
                                    cancellationToken: cancellationToken);
                                return;
                            }
                            curChat.Data = curChat.BufferData;
                            curChat.BufferData = null;
                            await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "Данные перезаписаны.",
                                    cancellationToken: cancellationToken);
                            break;
                        case "Отменить.":
                            curChat.BufferData = null;
                            break;
                        default:
                            break;
                    }
                    if (curChat.Data is not null)
                    {
                        await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Выберите действие с данными.",
                                replyMarkup: menuKeyboard,
                                cancellationToken: cancellationToken);
                    }
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