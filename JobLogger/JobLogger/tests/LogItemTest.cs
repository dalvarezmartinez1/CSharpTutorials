using System;
using NUnit.Framework;

namespace JobLogger.tests
{
    [TestFixture]
    public class LogItemTest
    {
        LogLevel logLevel;
        string dateString;
        string message;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            logLevel = LogLevel.ERROR;
            dateString = "20130909 10:09:10";
            message = "This is an error";
        }

        [Test]
        public void okLogItem()
        {
            LogItem logItem = new LogItem(logLevel, dateString, message);
            String logItemString = logItem.ToString();
            Assert.True(logItemString.Contains(logLevel.NAME) && logItemString.Contains(dateString) && logItemString.Contains(message));
        }

        [Test]
        public void logLevelNull()
        {
            LogItem logItem = new LogItem(null, dateString, message);
            String logItemString = logItem.ToString();
            Assert.True(!logItemString.Contains(logLevel.NAME) && logItemString.Contains(dateString) && logItemString.Contains(message));
        }

        [Test]
        public void dateNull()
        {
            LogItem logItem = new LogItem(logLevel, null, message);
            String logItemString = logItem.ToString();
            Assert.True(logItemString.Contains(logLevel.NAME) && !logItemString.Contains(dateString) && logItemString.Contains(message));
        }

        [Test]
        public void messageNull()
        {
            LogItem logItem = new LogItem(logLevel, dateString, null);
            String logItemString = logItem.ToString();
            Assert.True(logItemString.Contains(logLevel.NAME) && logItemString.Contains(dateString) && !logItemString.Contains(message));
        }
    }
}
