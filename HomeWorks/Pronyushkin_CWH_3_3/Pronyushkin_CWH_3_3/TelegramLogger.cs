using Microsoft.Extensions.Logging;

namespace Pronyushkin_CWH_3_3
{
    /// <summary>
    /// Класс предоставляет логер, который работает с файлами.
    /// </summary>
    public class TelegramLoggerProvider : ILoggerProvider
    {
        private readonly StreamWriter _logFileWriter;

        public TelegramLoggerProvider(StreamWriter logFileWriter)
        {
            _logFileWriter = logFileWriter ?? throw new ArgumentNullException(nameof(logFileWriter));
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TelegramLogger(categoryName, _logFileWriter);
        }

        public void Dispose()
        {
            _logFileWriter.Dispose();
        }
    }

    /// <summary>
    /// Класс логера, который работает с файлами.
    /// </summary>
    public class TelegramLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly StreamWriter _logFileWriter;

        public TelegramLogger(string categoryName, StreamWriter logFileWriter)
        {
            _categoryName = categoryName;
            _logFileWriter = logFileWriter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Information;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            // Отформатированное сообщение.
            var message = formatter(state, exception);

            // Записываем сообщение.
            _logFileWriter.WriteLine($"[{logLevel}] [{_categoryName}] {message}");
            _logFileWriter.Flush();
        }
    }
}
