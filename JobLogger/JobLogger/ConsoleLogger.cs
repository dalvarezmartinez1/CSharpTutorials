using System;

namespace JobLogger
{
    internal class ConsoleLogger : ILogger
    {
        public bool Log(LogItem logItem)
        {
            bool ret = false;
            if (logItem != null)
            {
                Console.ForegroundColor = logItem.LogLevel.Color;
                try
                {
                    System.Console.WriteLine(logItem.ToString());
                    ret = true;
                }
                catch (ArgumentNullException e)
                { // We should handle exceptions better, but right now I can't think of anything
                    Console.WriteLine(String.Format("Error {0} while logging {1}", e.ToString(), logItem.ToString()));
                }
            }
            return ret;
        }
    }
}
