using NUnit.Framework;

namespace JobLogger.tests
{
    // To be done SetUpFixture and TearDownFeature
    [TestFixture]
    public class FileLoggerTest
    {
        private LogItem logItemInfo;
        private LogItem logItemWarning;
        private LogItem logItemError;
        string EXISTING_FILE_PATH;
        string NON_EXISTING_FILE_PATH;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            logItemInfo = new LogItem(LogLevel.INFO, "20130909 10:09:10", "This is an info");
            logItemWarning = new LogItem(LogLevel.WARNING, "20130909 10:09:10", "This is a warning");
            logItemError = new LogItem(LogLevel.ERROR, "20130909 10:09:10", "This is an error");
            // Assign correct values and manage the file resources..to be done
            EXISTING_FILE_PATH = "C:\test.txt";
            NON_EXISTING_FILE_PATH = "C:\test\test.txt";
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            // Leave the properties and the resources to the pre-test state...do be done
        }

        [Test]
        public void logValidLogItemToExistingFilePath()
        {
            FileLogger fileLogger = new FileLogger(EXISTING_FILE_PATH);
            Assert.True(fileLogger.Log(logItemInfo));
            Assert.True(fileLogger.Log(logItemWarning));
            Assert.True(fileLogger.Log(logItemError));
        }

        [Test]
        public void logValidLogItemToNonExistingFilePath()
        {
            FileLogger fileLogger = new FileLogger(NON_EXISTING_FILE_PATH);
            Assert.False(fileLogger.Log(logItemInfo));
            Assert.False(fileLogger.Log(logItemWarning));
            Assert.False(fileLogger.Log(logItemError));
        }

        [Test]
        public void logNullLogItemToNullFilePath()
        {
            FileLogger fileLogger = new FileLogger(null);
            Assert.False(fileLogger.Log(null));
        }

        [Test]
        public void logNullLogItemToExistingFilePath()
        {
            FileLogger fileLogger = new FileLogger(EXISTING_FILE_PATH);
            Assert.False(fileLogger.Log(null));
        }

        [Test]
        public void logNullLogItemToNonExistingFilePath()
        {
            FileLogger fileLogger = new FileLogger(NON_EXISTING_FILE_PATH);
            Assert.False(fileLogger.Log(null));
        }
    }
}
