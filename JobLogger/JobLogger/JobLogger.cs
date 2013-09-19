using System;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
namespace JobLogger
{
    public class JobLogger
    {
        private static readonly string DATE_FORMAT = "dd/MM/yyyy HH:mm:ss";

        public LogLevel LogLevel { get; set; }
        private ILogger[] loggers;

        public JobLogger( LogLevel logLevel, params LogDestination[] logDestinations)
        {
            LogLevel = logLevel;
            loggers = LoggerFactory.getLoggers(logDestinations);
        }

        public void LogMessage(LogLevel logLevel, string message)
        {
            if (isAllowed(logLevel))
            {
                LogItem logItem = new LogItem(logLevel, DateTime.Now.ToString(DATE_FORMAT), message);
                for (int i = 0; i < loggers.Length; i++)
                {
                    loggers[i].Log(logItem);
                }
            }
        }

        private bool isAllowed(LogLevel logLevel)
        {
            return logLevel.VAL >= LogLevel.VAL;
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

    // Better than enum, order of declaration does not matter here
    public class LogLevel
    {
        public int VAL { get; private set; }
        public string NAME { get; private set; }

        private LogLevel(int val, string name) {
            VAL = val;
            NAME = name;
        }

        public static readonly LogLevel INFO = new LogLevel(1, "INFO");
        public static readonly LogLevel WARNING = new LogLevel(2, "WARNING");
        public static readonly LogLevel ERROR = new LogLevel(3, "ERROR");
    }
}