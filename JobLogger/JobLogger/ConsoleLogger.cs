using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger
{
    internal class ConsoleLogger : ILogger
    {
        private static readonly ConsoleColor DEFAULT_COLOR = ConsoleColor.White;
        private static readonly Dictionary<LogLevel, ConsoleColor> level2ColorMap;

        static ConsoleLogger()
        {
            level2ColorMap = new Dictionary<LogLevel, ConsoleColor>();
            level2ColorMap.Add(LogLevel.INFO, ConsoleColor.Green);
            level2ColorMap.Add(LogLevel.WARNING, ConsoleColor.Yellow);
            level2ColorMap.Add(LogLevel.ERROR, ConsoleColor.Red);
        }

        public void Log(LogItem logItem)
        {
            setColor(logItem.LogLevel);
            try
            {
                System.Console.WriteLine(logItem.ToString());
            }
            catch (ArgumentNullException e)
            { // We should handle exceptions better, but right now I can't think of anything
                Console.WriteLine(String.Format("Error {0} while logging {1}", e.ToString(), logItem.ToString()));
            }
        }

        private void setColor(LogLevel logLevel)
        {
            ConsoleColor color = DEFAULT_COLOR;
            if (level2ColorMap.TryGetValue(logLevel, out color))
            {
                Console.ForegroundColor = color;
            }
        }
    }
}
