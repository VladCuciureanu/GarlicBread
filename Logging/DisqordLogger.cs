using System;
using System.Collections.Generic;
using Disqord.Logging;
using Microsoft.Extensions.Logging;
using ILogger = Disqord.Logging.ILogger;

namespace GarlicBread.Logging
{
    public class DisqordLogger : ILogger
    {
        private readonly ILoggerFactory _factory;

        private readonly Dictionary<LogSeverity, LogLevel> _map = new Dictionary<LogSeverity, LogLevel>()
        {
            {LogSeverity.Trace, LogLevel.Trace},
            {LogSeverity.Debug, LogLevel.Debug},
            {LogSeverity.Information, LogLevel.Information},
            {LogSeverity.Warning, LogLevel.Warning},
            {LogSeverity.Error, LogLevel.Error}
        };

        public DisqordLogger(ILoggerFactory factory)
        {
            _factory = factory;
        }

        public void Log(object sender, LogEventArgs e)
        {
            _factory.CreateLogger("Discord." + e.Source).Log(_map[e.Severity], e.Exception, e.Message);
        }

        #pragma warning disable 67
        public event EventHandler<LogEventArgs> Logged;

        public void Dispose()
        {
        }
    }
}