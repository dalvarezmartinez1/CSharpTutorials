using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger
{
    // If you want to add another Logger, just add another case.
    internal class LoggerFactory
    {
        internal static ILogger[] getLoggers(LogDestination[] logDestination)
        {
            ILogger[] loggers = new ILogger[logDestination.Length];
            for (int i = 0; i < logDestination.Length; i++)
            {
                switch (logDestination[i])
                {
                    case LogDestination.CONSOLE:
                        loggers[i] = new ConsoleLogger();
                        break;
                    case LogDestination.FILE:
                        loggers[i] = new FileLogger();
                        break;
                    case LogDestination.DATABASE:
                        loggers[i] = new DatabaseLogger();
                        break;
                    default:
                        Console.WriteLine("Logger not supported " + logDestination[i]);
                        break;
                }
            }
            return loggers;
        }
    }

    public enum LogDestination
    {
        FILE,
        CONSOLE,
        DATABASE
    }
}
