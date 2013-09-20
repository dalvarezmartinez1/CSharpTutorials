using NUnit.Framework;

namespace JobLogger.tests
{
    [TestFixture]
    public class JobLoggerTest
    {
        LogLevel jobLoggerlogLevel;
        LogDestination[] logDestinations;
        private LogItem logItemInfo;
        private LogItem logItemWarning;
        private LogItem logItemError;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            jobLoggerlogLevel = LogLevel.WARNING;
            logDestinations = new LogDestination[] { LogDestination.CONSOLE, LogDestination.FILE};
            logItemInfo = new LogItem(LogLevel.INFO, "20130909 10:09:10", "This is an info");
            logItemWarning = new LogItem(LogLevel.WARNING, "20130909 10:09:10", "This is a warning");
            logItemError = new LogItem(LogLevel.ERROR, "20130909 10:09:10", "This is an error");
        }

        [Test]
        public void log()
        {
            JobLogger jobLogger = new JobLogger(jobLoggerlogLevel, logDestinations);
            Assert.False(jobLogger.LogMessage(logItemInfo.LogLevel, logItemInfo.Message));
            Assert.True(jobLogger.LogMessage(logItemWarning.LogLevel, logItemWarning.Message));
            Assert.True(jobLogger.LogMessage(logItemError.LogLevel, logItemError.Message));
            // We could add some more, and more variations for LogDestination array, as when it's empty...
        }


        [Test]
        public void logLogDestinationsNull()
        {
            JobLogger jobLogger = new JobLogger(jobLoggerlogLevel, null);
            Assert.False(jobLogger.LogMessage(logItemInfo.LogLevel, logItemInfo.Message));
            Assert.False(jobLogger.LogMessage(logItemWarning.LogLevel, logItemWarning.Message));
            Assert.False(jobLogger.LogMessage(logItemError.LogLevel, logItemError.Message));
        }

        [Test]
        public void logLogLevelAndDestinationsNull()
        {
            JobLogger jobLogger = new JobLogger(null, null);
            Assert.False(jobLogger.LogMessage(logItemInfo.LogLevel, logItemInfo.Message));
            Assert.False(jobLogger.LogMessage(logItemWarning.LogLevel, logItemWarning.Message));
            Assert.False(jobLogger.LogMessage(logItemError.LogLevel, logItemError.Message));
        }

        [Test]
        public void logJobLoggerLogLevelNull()
        {
            JobLogger jobLogger = new JobLogger(null, logDestinations);
            Assert.False(jobLogger.LogMessage(logItemInfo.LogLevel, logItemInfo.Message));
            Assert.False(jobLogger.LogMessage(logItemWarning.LogLevel, logItemWarning.Message));
            Assert.False(jobLogger.LogMessage(logItemError.LogLevel, logItemError.Message));
        }

        [Test]
        public void logLogLevelNull()
        {
            JobLogger jobLogger = new JobLogger(jobLoggerlogLevel, logDestinations);
            Assert.False(jobLogger.LogMessage(null, logItemInfo.Message));
            Assert.False(jobLogger.LogMessage(null, logItemWarning.Message));
            Assert.False(jobLogger.LogMessage(null, logItemError.Message));
        }

        [Test]
        public void logMessageNull()
        {
            JobLogger jobLogger = new JobLogger(jobLoggerlogLevel, logDestinations);
            Assert.False(jobLogger.LogMessage(logItemInfo.LogLevel, null));
            Assert.False(jobLogger.LogMessage(logItemWarning.LogLevel, null));
            Assert.False(jobLogger.LogMessage(logItemError.LogLevel, null));
        }
    }
}
