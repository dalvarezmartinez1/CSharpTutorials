using NUnit.Framework;

namespace JobLogger.tests
{
    [TestFixture]
    public class ConsoleLoggerTest
    {
        private LogItem logItemInfo;
        private LogItem logItemWarning;
        private LogItem logItemError;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            logItemInfo = new LogItem(LogLevel.INFO, "20130909 10:09:10", "This is an info");
            logItemWarning = new LogItem(LogLevel.WARNING, "20130909 10:09:10", "This is a warning");
            logItemError = new LogItem(LogLevel.ERROR, "20130909 10:09:10", "This is an error");
        }

        [Test]
        public void logValidLogItem()
        {
            ConsoleLogger consoleLogger = new ConsoleLogger();
            Assert.True(consoleLogger.Log(logItemInfo));
            Assert.True(consoleLogger.Log(logItemWarning));
            Assert.True(consoleLogger.Log(logItemError));
        }

        [Test]
        public void logNullLogItem()
        {
            ConsoleLogger consoleLogger = new ConsoleLogger();
            Assert.False(consoleLogger.Log(null));
        }
    }
}
