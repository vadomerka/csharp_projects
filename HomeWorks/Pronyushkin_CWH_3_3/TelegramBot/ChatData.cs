using DataProcessing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    /// <summary>
    /// Класс, который хранит информацию о чатах.
    /// </summary>
    public class ChatData
    {
        private long _chatId;
        // Список данных.
        private List<IStreamItem>? _data = null;
        // Список считанных данных, которые могут перезаписать основной.
        private List<IStreamItem>? _bufferData = null;
        // Столбцы, по которым будет проходить выборка.
        private string[] _fetchCols = new string[2];
        // Считанные данные для выборки от пользователя.
        private List<string> _fetchedValues = new List<string>();
        // Необходимое количество данных, которое нужно считать.
        private int _fetchCount = 0;

        public ChatData() { }
        public ChatData(long id) { _chatId = id; }
        public long Id { get => _chatId; set { } }
        public string[] FetchedCols { get => _fetchCols; set { _fetchCols = value; } }
        public List<string> FetchedValues { get => _fetchedValues; set { _fetchedValues = value; } }
        public int FetchCount { get => _fetchCount; set { _fetchCount = value; } }
        public List<IStreamItem>? Data { get => _data; set { _data = value; } }
        public List<IStreamItem>? BufferData { get => _bufferData; set { _bufferData = value; } }

        /// <summary>
        /// Метод сообщает, нужно ли отлавливать значения для выборки.
        /// </summary>
        /// <returns>true - если да, иначе - false</returns>
        public bool IsFetching()
        {
            if (_fetchedValues.Count < _fetchCount) return true;
            return false;
        }

        /// <summary>
        /// Возвращает все растения в данных.
        /// </summary>
        /// <returns>Список растений.</returns>
        public List<Plant> GetPlants()
        {
            if (_data == null) return new List<Plant>();
            return _data.Where(p => p is Plant).Select(p => (Plant)p).ToList() ??
                            new List<Plant>();
        }

        /// <summary>
        /// Возвращает все заголовки в данных.
        /// </summary>
        /// <returns>Список заголовков.</returns>
        public List<Header> GetHeaders()
        {
            if (_data == null) return new List<Header>();
            return _data.Where(p => p is Header).Select(p => (Header)p).ToList() ??
                            new List<Header>();
        }

        /// <summary>
        /// Обновляет сохраненные данные
        /// </summary>
        /// <param name="plants">Список объектов на которые нужно обновить данные.</param>
        public void UpdatePlants(List<Plant> plants)
        {
            var res = new List<IStreamItem>();
            foreach (var item in GetHeaders())
            {
                res.Add(item);
            }
            foreach (var item in plants)
            {
                res.Add(item);
            }
            _data = res;
        }
    }
}
