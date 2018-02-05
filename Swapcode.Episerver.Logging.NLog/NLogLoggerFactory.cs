using NLog;
using Epilog = EPiServer.Logging;

namespace Swapcode.Episerver.Logging.NLog
{
    public class NLogLoggerFactory : Epilog.ILoggerFactory
    {
        public Epilog.ILogger Create(string name)
        {
            // TODO: should we throw argumentexception when null, empty or white spaces?

            return new NLogLogger(LogManager.GetLogger(name));
        }
    }
}
