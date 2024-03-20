using DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBot
{
    public static class TGHelper
    {
        public static List<Plant> FetchPlants(ChatData curChat)
        {
            var plants = curChat.GetPlants();
            List<Plant> newPlants = plants;
            for (int i = 0; i < curChat.FetchCount; i++)
            {
                if (curChat.FetchedCols[i] == "LandscapingZone")
                {
                    newPlants = plants.Where(x => x.LandscapingZone == curChat.FetchedValues[i]).ToList();
                }
                if (curChat.FetchedCols[i] == "LocationPlace")
                {
                    newPlants = plants.Where(x => x.LocationPlace == curChat.FetchedValues[i]).ToList();
                }
                if (curChat.FetchedCols[i] == "ProsperityPeriod")
                {
                    newPlants = plants.Where(x => x.ProsperityPeriod == curChat.FetchedValues[i]).ToList();
                }
                if (newPlants.Count <= 0) throw new ArgumentNullException();
                plants = newPlants;
            }
            return plants;
        }

        public static async Task<ChatData> GetCurChat(ITelegramBotClient botClient, long chatId, List<ChatData> chatsData,
                                                        CancellationToken cancellationToken)
        {
            var curChat = chatsData.Find(x => x.Id == chatId);
            if (curChat is null)
            {
                curChat = new ChatData(chatId);
                chatsData.Add(curChat);
                if (botClient is null) return curChat;
                await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Вас приветствует телеграм бот!\nЧтобы начать работу, отправьте файл с данными.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
            }
            return curChat;
        }

        public static async Task<bool> CurChatCheck(ITelegramBotClient botClient, ChatData curChat,
                                                        CancellationToken cancellationToken, string message = "Загрузите данные.")
        {
            if (botClient is null) return true;
            if (curChat.Data is null)
            {
                await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: message,
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
                return true;
            }
            return false;
        }

        public static async Task SendStreamFile(Stream fileStream, ChatData curChat, ITelegramBotClient botClient, 
                                                    string fileName = "plants.csv")
        {
            if (botClient is null) return;
            fileStream.Seek(0, SeekOrigin.Begin);
            await botClient.SendDocumentAsync(
                chatId: curChat.Id,
                document: InputFile.FromStream(stream: fileStream, fileName: fileName),
                caption: "Обработанные данные:");
            fileStream.Seek(0, SeekOrigin.Begin);
        }

    }
}
