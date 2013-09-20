using System;
using System.Text;

namespace JobLogger
{
    internal class LogItem
    {
        private static readonly string MSG_FORMAT = "{0}  {1}  {2}";

        internal LogLevel LogLevel { get; set; }
        internal string DateString { get; set; }
        internal string Message { get; set; }

        internal LogItem(LogLevel logLevel, String dateString, String message)
        {
            LogLevel = logLevel;
            DateString = dateString;
            Message = message;
        }

        public override string ToString()
        {
            string strLogLevel = (LogLevel == null) ? "" : LogLevel.NAME;
            string strDateString = (DateString == null) ? "" : DateString;
            string strMessage = (Message == null) ? "" : Message;
            return String.Format(MSG_FORMAT, strLogLevel, strDateString, strMessage);
        }
    }
}
