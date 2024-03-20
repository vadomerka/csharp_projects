using DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class TGCommandHandler
    {
        public static async Task FetchCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
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
        }

        public static async Task SortCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                chatId: curChat.Id,
                text: "В каком порядке вы хотите отсортировать объекты?\nСортировка будет происходить по полю LatinName",
                replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
                    new[] {
                        InlineKeyboardButton.WithCallbackData("В прямом.", "sort/straight"),
                        InlineKeyboardButton.WithCallbackData("В обратном.", "sort/reverse"),
                    }}),
                cancellationToken: cancellationToken);
        }

        public static async Task DownloadCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
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
        }

        public static async Task DownloadCSVCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            if (await TGHelper.CurChatCheck(botClient, curChat, cancellationToken) || curChat.Data is null) return;
            try
            {
                CSVProcessing cp = new CSVProcessing();
                using (Stream fileStream = await cp.WriteAsync(curChat.Data))
                {
                    if (fileStream is null) throw new ArgumentNullException();
                    await TGHelper.SendStreamFile(fileStream, curChat, botClient);
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: $"Произошла ошибка.\n{ex.Message}",
                    cancellationToken: cancellationToken);
            }
        }

        public static async Task DownloadJSONCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            try
            {
                JSONProcessing jp = new JSONProcessing();
                var plants = curChat.GetPlants();
                using (Stream fileStream = await jp.WriteAsync(plants))
                {
                    if (fileStream is null) throw new ArgumentNullException();
                    await TGHelper.SendStreamFile(fileStream, curChat, botClient, "plants.json");
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: $"Произошла ошибка.\n{ex.Message}",
                    cancellationToken: cancellationToken);
            }
        }

        public static async Task RewriteCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            curChat.Data = curChat.BufferData;
            curChat.BufferData = null;
            await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: "Данные перезаписаны.",
                    cancellationToken: cancellationToken);
        }
    }
}
