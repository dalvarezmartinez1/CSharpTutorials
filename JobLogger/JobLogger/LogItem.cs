using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLogger
{
    internal class LogItem
    {
        private static readonly string MSG_FORMAT = "{0}  {1}  {2}";

        public LogLevel LogLevel { get; set; }
        public string DateString { get; set; }
        public string Message { get; set; }

        public LogItem(LogLevel logLevel, String dateString, String message)
        {
            LogLevel = logLevel;
            DateString = dateString;
            Message = message;
        }

        public override string ToString()
        {
            return String.Format(MSG_FORMAT, LogLevel.NAME, DateString, Message);
        }
    }
}
