using Microsoft.Extensions.Logging;
using System;

namespace Erp.WebApp.Services
{
    public class SimpleLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                var message = formatter(state, exception);
                System.Diagnostics.Debug.WriteLine($"[{logLevel}] {typeof(T).Name}: {message}");
                if (exception != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception: {exception}");
                }
            }
        }
    }
}