using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
namespace JobLogger
{

    internal class LoggerFactory
    {
        // If you want to add another Logger, just add another case.
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
