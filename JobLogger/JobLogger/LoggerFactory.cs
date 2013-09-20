using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
namespace JobLogger
{
    internal class LoggerFactory
    {
        private static readonly string FILE_LOG_DIR = ConfigurationManager.AppSettings["LogFileDirectory"];
        // We will be able to create 1 different file every second, I assume desired behavior
        private static readonly string FILE_DATE_FORMAT = "yyyyMMdd-HHmmss";

        // If you want to add another Logger, just add another case.
        internal static ILogger[] getLoggers(params LogDestination[] logDestinations)
        {
            ILogger[] loggers;
            if (logDestinations != null)
            {
                LogDestination[] distinctLogDest = logDestinations.Distinct().ToArray();
                loggers = new ILogger[distinctLogDest.Length];
                for (int i = 0; i < distinctLogDest.Length; i++)
                {
                    switch (distinctLogDest[i])
                    {
                        case LogDestination.CONSOLE:
                            loggers[i] = new ConsoleLogger();
                            break;
                        case LogDestination.FILE:
                            string filePath = FILE_LOG_DIR + "\\LogFile" + DateTime.Now.ToString(FILE_DATE_FORMAT) + ".txt";
                            loggers[i] = new FileLogger(filePath);
                            break;
                        case LogDestination.DATABASE:
                            loggers[i] = new DatabaseLogger(new Model(ConfigurationManager.AppSettings["ConnectionString"]));
                            break;
                        default:
                            Console.WriteLine("Logger not supported " + distinctLogDest[i]);
                            break;
                    }
                }
            }
            else
            {
                loggers = new ILogger[0];
            }
            return loggers;
        }

        /*
        // If you want to add another ILogger, just create one, this method leverages getLogDestination from ILogger
        internal static ILogger[] getLoggers2(params LogDestination[] destinations)
        {
            var iType = typeof(ILogger);
            Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                        .Where(p => (iType.IsAssignableFrom(p) && p != iType)).ToArray<Type>();

            List<ILogger> ret = filterByLogDestination(destinations, createILoggersFromTypes(types));
            return ret.ToArray();
        }

        private static List<ILogger> filterByLogDestination(LogDestination[] destinations, List<ILogger> loggers)
        {
            List<ILogger> list = new List<ILogger>();
            foreach(ILogger iLogger in loggers)
            {
                if (destinations.Contains(iLogger.getLogDestination()))
                {
                    list.Add(iLogger);
                }
            }
            return list;
        }

        private static List<ILogger> createILoggersFromTypes(Type[] types)
        {
            List<ILogger> ret = new List<ILogger>();
            for (int i = 0; i < types.Length; i++)
            {
                try
                {
                    ret.Add((ILogger) Activator.CreateInstance(types[i]));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot create instance for " + types[i].Name);
                }
            }
            return ret;
        }
         * */
    }

    public enum LogDestination
    {
        FILE,
        CONSOLE,
        DATABASE
    }
}
