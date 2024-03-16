using DataProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class ChatData
    {
        private long _chatId;
        private List<ICSVItem>? _data = null;
        private List<ICSVItem>? _bufferData = null;
        public ChatData() { }
        public ChatData(long id) { _chatId = id; }
        public long Id { get => _chatId; set { } }
        public List<ICSVItem>? Data { get => _data; set { _data = value; } }
        public List<ICSVItem>? BufferData { get => _bufferData; set { _bufferData = value; } }
    }
}
