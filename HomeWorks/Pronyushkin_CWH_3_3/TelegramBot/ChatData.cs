using DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ChatData
    {
        private long _chatId;
        private List<ICSVItem>? _data = null;
        private List<ICSVItem>? _bufferData = null;
        private string[] _fetchCols = new string[2];
        private List<string> _fetchedValues = new List<string>();
        private int _fetchCount = 0;

        public ChatData() { }
        public ChatData(long id) { _chatId = id; }
        public long Id { get => _chatId; set { } }
        public string[] FetchedCols { get => _fetchCols; set { _fetchCols = value; } }
        public List<string> FetchedValues { get => _fetchedValues; set { _fetchedValues = value; } }
        public int FetchCount { get => _fetchCount; set { _fetchCount = value; } }
        public List<ICSVItem>? Data { get => _data; set { _data = value; } }
        public List<ICSVItem>? BufferData { get => _bufferData; set { _bufferData = value; } }

        public bool IsFetching()
        { 
            if (_fetchedValues.Count < _fetchCount) return true;
            return false;
        }

        public List<Plant> GetPlants()
        {
            if (_data == null) return new List<Plant>();
            return _data.Where(p => p is Plant).Select(p => (Plant)p).ToList() ??
                            new List<Plant>();
        }

        public List<Header> GetHeaders()
        {
            if (_data == null) return new List<Header>();
            return _data.Where(p => p is Header).Select(p => (Header)p).ToList() ??
                            new List<Header>();
        }

        public void UpdatePlants(List<Plant> plants)
        {
            var res = new List<ICSVItem>();
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
