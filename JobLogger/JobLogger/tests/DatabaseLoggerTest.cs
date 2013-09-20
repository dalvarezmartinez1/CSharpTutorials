using NUnit.Framework;

namespace JobLogger.test
{
    [TestFixture]
    public class DatabaseLoggerTest
    {
        private LogItem logItemInfo;
        private LogItem logItemWarning;
        private LogItem logItemError;
        private Model okModel;
        private Model fakeModel;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            logItemInfo = new LogItem(LogLevel.INFO, "20130909 10:09:10", "This is an info");
            logItemWarning = new LogItem(LogLevel.WARNING, "20130909 10:09:10", "This is a warning");
            logItemError = new LogItem(LogLevel.ERROR, "20130909 10:09:10", "This is an error");
            okModel = new Model("Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;");
            fakeModel = new Model("FakeConnString");
        }

        [Test]
        public void logValidLogItemToOkModel()
        {
            DatabaseLogger dbLogger = new DatabaseLogger(okModel);
            Assert.True(dbLogger.Log(logItemInfo));
            Assert.True(dbLogger.Log(logItemWarning));
            Assert.True(dbLogger.Log(logItemError));
        }

        [Test]
        public void logValidLogItemToFakeModel()
        {
            DatabaseLogger dbLogger = new DatabaseLogger(fakeModel);
            Assert.False(dbLogger.Log(logItemInfo));
            Assert.False(dbLogger.Log(logItemWarning));
            Assert.False(dbLogger.Log(logItemError));
        }

        [Test]
        public void logNullLogItemToNullModel()
        {
            DatabaseLogger dbLogger = new DatabaseLogger(null);
            Assert.False(dbLogger.Log(null));
        }

        [Test]
        public void logNullLogItemToOkModel()
        {
            DatabaseLogger dbLogger = new DatabaseLogger(okModel);
            Assert.False(dbLogger.Log(null));
        }

        [Test]
        public void logNullLogItemToFakeModel()
        {
            DatabaseLogger dbLogger = new DatabaseLogger(fakeModel);
            Assert.False(dbLogger.Log(null));
        }
    }
}
