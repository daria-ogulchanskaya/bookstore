using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace Bookstore.Core.Common
{
    public class FileLogger : ILogger
    {
        private readonly string _path;

        public FileLogger(string path)
        {
            _path = path;
        }

        public void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var logMessage = new StringBuilder();

            using StreamWriter logWriter = new StreamWriter(File.Open(_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite));
            logWriter.BaseStream.Position = logWriter.BaseStream.Length;
            logMessage.AppendLine($"[{DateTime.UtcNow}]:");

            if (!(exception is null))
            {
                logMessage.AppendLine(@$"Exception message: {exception.Message}\r\n
--------------------------------------------------\n
Exception stacktrace: {exception.StackTrace}\r");

                if (!(exception.InnerException is null))
                {
                    logMessage.AppendLine(@$"--------------------------------------------------\n
Inner exception message: {exception.InnerException.Message}\r\n--------------------------------------------------\n
Inner exception stacktrace: {exception.InnerException.StackTrace}\r\n
--------------------------------------------------");
                }
            }

            if (!(formatter is null))
            {
                logMessage.AppendLine($"Message: {formatter(state, exception)}\r\n--------------------------------------------------");
            }

            logMessage.AppendLine("==================================================\r\n");
            logWriter.WriteLine(logMessage.ToString());
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }
    }
}
