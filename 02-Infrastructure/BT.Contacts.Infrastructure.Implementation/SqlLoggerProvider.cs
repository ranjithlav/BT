using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace BT.Contacts.Infrastructure.Implementation
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        { }

        private class CustomLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (state.ToString().Contains("Executing DbCommand"))
                {
                    File.AppendAllText(@"D:\logs\BT.Contacts\sqllog.txt", formatter(state, exception));
                    Console.WriteLine(formatter(state, exception));
                }
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}
