using DataProcessing;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBot.InnerHandlers
{
    /// <summary>
    /// Вспомогательный класс. Хранит дополнительные методы.
    /// </summary>
    public static class TGHelper
    {
        /// <summary>
        /// Метод отфильтровывает список по собранным значениям пользователя.
        /// </summary>
        /// <param name="curChat">Текущий чат.</param>
        /// <returns>Отфильтрованный список.</returns>
        /// <exception cref="ArgumentNullException">Если результат пуст, возвращаем ошибку.</exception>
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

        /// <summary>
        /// Метод ищет чат в базе данных. Если не находит, создает его.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="chatId">Id чата.</param>
        /// <param name="chatsData">Информация о чатах.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ссылку на текущий чат.</returns>
        public static async Task<ChatData> GetCurChat(ITelegramBotClient botClient, long chatId, List<ChatData> chatsData,
                                                        CancellationToken cancellationToken)
        {
            var curChat = chatsData.Find(x => x.Id == chatId);
            if (curChat is null)
            {
                // Если чата не существовало, создаем его.
                curChat = new ChatData(chatId);
                chatsData.Add(curChat);
                if (botClient is null) return curChat;
                // Посылаем приветственное сообщение.
                await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Вас приветствует телеграм бот!\nЧтобы начать работу, отправьте файл с данными.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
            }
            return curChat;
        }

        /// <summary>
        /// Метод проверяет, загружен ли файл в базу данных. И если нет - отправляет предупреждение
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>true если файл не загружен, иначе false.</returns>
        public static async Task<bool> CurChatDataCheck(ITelegramBotClient botClient, ChatData curChat,
                                                        CancellationToken cancellationToken)
        {
            if (botClient is null) return true;
            if (curChat.Data is null)
            {
                await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: "Загрузите данные.",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод отправляет файл из потока в чат.
        /// </summary>
        /// <param name="fileStream">Поток с файлом.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task SendStreamFile(Stream fileStream, ChatData curChat, ITelegramBotClient botClient,
                                                        CancellationToken cancellationToken, string fileName = "plants.csv")
        {
            if (botClient is null) return;
            fileStream.Seek(0, SeekOrigin.Begin);
            await botClient.SendDocumentAsync(
                chatId: curChat.Id,
                document: InputFile.FromStream(stream: fileStream, fileName: fileName),
                caption: "Обработанные данные:",
                cancellationToken: cancellationToken);
            fileStream.Seek(0, SeekOrigin.Begin);
            await SendMenuMessage(botClient, curChat, cancellationToken);
        }

        /// <summary>
        /// Метод отправляет сообщение с меню.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task SendMenuMessage(ITelegramBotClient botClient, ChatData curChat,
                                                        CancellationToken cancellationToken)
        {
            // Кнопки меню.
            ReplyKeyboardMarkup menuKeyboard = new(new[]
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
            await botClient.SendTextMessageAsync(
                chatId: curChat.Id,
                text: "Выберите действие с данными.",
                replyMarkup: menuKeyboard,
                cancellationToken: cancellationToken);
        }
    }
}
