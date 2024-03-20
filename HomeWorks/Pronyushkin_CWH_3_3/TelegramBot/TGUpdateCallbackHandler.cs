using DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class TGUpdateCallbackHandler
    {
        public static async Task HandleCallbacksAsync(ITelegramBotClient botClient, Update update,
            List<ChatData> chatsData, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery is null || update.CallbackQuery.Message is null) return;
            var chatId = update.CallbackQuery.Message.Chat.Id;
            var curChat = await TGHelper.GetCurChat(botClient, chatId, chatsData, cancellationToken);

            if (await TGHelper.CurChatCheck(botClient, curChat, cancellationToken)
                || curChat.Data is null) return;
            var plants = curChat.GetPlants();
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
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
                    break;
                case "fetch/LocationPlace":
                    curChat.FetchCount = 1;
                    curChat.FetchedValues = new List<string>();
                    curChat.FetchedCols = new string[] { "LocationPlace", "" };
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Введите значение по которому нужно отфильтровать данные.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
                    break;
                case "fetch/LandscapingZone&ProsperityPeriod":
                    curChat.FetchCount = 2;
                    curChat.FetchedValues = new List<string>();
                    curChat.FetchedCols = new string[] { "LandscapingZone", "ProsperityPeriod" };
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Введите значение по которому нужно отфильтровать данные.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
                    break;
                default: 
                    break;
            }
            curChat.UpdatePlants(plants);
            await botClient.AnswerCallbackQueryAsync(update.CallbackQuery.Id, cancellationToken: cancellationToken);
        }
    }
}
