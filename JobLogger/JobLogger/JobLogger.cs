using System;
namespace JobLogger
{
    public sealed class JobLogger
    {
        private static readonly string DATE_FORMAT = "dd/MM/yyyy HH:mm:ss";

        private LogLevel logLevel;
        private ILogger[] loggers;

        public JobLogger(LogLevel logLevel, params LogDestination[] logDestinations)
        {
            this.logLevel = logLevel;
            loggers = LoggerFactory.getLoggers(logDestinations);
        }

        public bool LogMessage(LogLevel logLevel, string message)
        {
            bool ret = false;
            if (logLevel != null && message != null)
            {
                if (isAllowed(logLevel))
                {
                    LogItem logItem = new LogItem(logLevel, DateTime.Now.ToString(DATE_FORMAT), message);
                    for (int i = 0; i < loggers.Length; i++)
                    {
                        ret = loggers[i].Log(logItem);
                    }
                }
            }
            return ret;
        }

        private bool isAllowed(LogLevel logLevel)
        {
            bool ret = false;
            if (logLevel != null)
            {
                ret = logLevel.VAL >= this.logLevel.VAL;
            }
            return ret;
        }

        public LogLevel getLogLevel() {
            return logLevel;
        }

        public bool setLogLevel(LogLevel logLevel)
        {
            bool ret = false;
            if (logLevel != null)
            {
                this.logLevel = logLevel;
                ret = true;
            }
            return ret;
        }
        
        public static void Main(String[] args)
        {
            JobLogger jobLogger = new JobLogger(LogLevel.ERROR, LogDestination.DATABASE, LogDestination.FILE, LogDestination.CONSOLE);

            jobLogger.LogMessage(LogLevel.WARNING, "This is a warning");
            jobLogger.LogMessage(LogLevel.ERROR, "This is an error");
            jobLogger.LogMessage(LogLevel.INFO, "This is an info");
            System.Console.ReadKey();
        }
        
    }

    /* Better than enum, order of declaration does not matter here.
     We can argue, whether is good to include the color here, if it's only used by the ConsoleLogger.
     This decision was made to maintain the SingleResponsibilityPrinciple. If we didn't include the color here, we would
     have to modify this class here, to include the new LogLevel,and also the ConsoleLogger class to map the loglevel
     to a specific color */
    public class LogLevel
    {
        internal int VAL { get; private set; }
        internal string NAME { get; private set; }
        internal ConsoleColor Color { get; private set; }

        private LogLevel(int val, string name, ConsoleColor color)
        {
            VAL = val;
            NAME = name;
            Color = color;
        }

        public static readonly LogLevel INFO = new LogLevel(1, "INFO", ConsoleColor.Green);
        public static readonly LogLevel WARNING = new LogLevel(2, "WARNING", ConsoleColor.Yellow);
        public static readonly LogLevel ERROR = new LogLevel(3, "ERROR", ConsoleColor.Red);
    }
}