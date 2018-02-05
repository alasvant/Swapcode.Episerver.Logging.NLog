using NLog;
using System;
using Epilog = EPiServer.Logging;

namespace Swapcode.Episerver.Logging.NLog
{
    /// <summary>
    /// Wrapper for NLog.ILogger instance.
    /// </summary>
    public class NLogLogger : Epilog.ILogger
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new NLog.ILogger wrapper instance.
        /// </summary>
        /// <param name="logger">NLog.ILogger</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="logger"/> is null</exception>
        public NLogLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Checks whether logging is enabled for the specified level.
        /// </summary>
        /// <param name="level">logging level</param>
        /// <returns>true if logging for the level is enabled otherwise false</returns>
        public bool IsEnabled(Epilog.Level level)
        {
            return _logger.IsEnabled(MapEpiserverLogLevel(level));
        }

        /// <summary>
        /// Writes to log if the used log level is enabled.
        /// </summary>
        public void Log<TState, TException>(Epilog.Level level, TState state, TException exception, Func<TState, TException, string> messageFormatter, Type boundaryType) where TException : Exception
        {
            // currently the episerver logging extensions [public static void Log(this ILogger logger, Level level, string message)] checks the log message with IsNullOrEmpty and don't log if true

            LogLevel logLevel = MapEpiserverLogLevel(level);

            // we are not calling the IsEnabled because the underlying implementations will do that
            // and the message is extracted when it is sure it will be actually logged
            // side note: the extension used here checks should the message be logged and then calls other method which in turn checks should the message be logged ;)
            // so even if we call directly the actual logging method we need to then call here the IsEnabled before creating the log message, so still two calls to IsEnabled(level)

            _logger.Log(logLevel, exception, () => {
                string logMessage = null;

                if (messageFormatter != null)
                {
                    try
                    {
                        logMessage = messageFormatter(state, exception);
                    }
                    catch (Exception)
                    {
                    }
                }

                // just a fallback to get something to the log in case the messageFormatter is null or has caused an exception
                if (string.IsNullOrWhiteSpace(logMessage))
                {
                    // Note: state can be value or reference type as the messageFormatter delegate doesn't restrict the TState to class
                    // for example if state is int with value 6 the state.ToString() is called
                    logMessage = state?.ToString() ?? exception?.Message;
                }

                return logMessage;
            });
        }

        /// <summary>
        /// Maps Episerver.Logging.Level enum value to NLog.LogLevel. For unknown Episerver.Logging.Level returns NLog.LogLevel.Info log level.
        /// </summary>
        /// <param name="level">Episerver.Logging.Level enum value</param>
        /// <returns>Mapped NLog.LogLevel</returns>
        public static LogLevel MapEpiserverLogLevel(Epilog.Level level)
        {
            switch (level)
            {
                case Epilog.Level.Trace:
                    return LogLevel.Trace;
                case Epilog.Level.Debug:
                    return LogLevel.Debug;
                case Epilog.Level.Information:
                    return LogLevel.Info;
                case Epilog.Level.Warning:
                    return LogLevel.Warn;
                case Epilog.Level.Error:
                    return LogLevel.Error;
                case Epilog.Level.Critical:
                    return LogLevel.Fatal;
                default:
                    // don't throw exception but default to info level if unknown level
                    return LogLevel.Info;
            }
        }
    }
}
