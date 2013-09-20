using System;

namespace JobLogger
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(LogItem logItem)
        {
            Console.ForegroundColor = logItem.LogLevel.Color;
            System.Console.WriteLine(logItem.ToString());
        }
    }
}
