using NUnit.Framework;

namespace JobLogger.tests
{
    [TestFixture]
    public class ModelTest
    {
        string okConnString;
        string fakeConnString;
        private LogItem logItemInfo;
        private LogItem logItemWarning;
        private LogItem logItemError;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            okConnString = "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
            fakeConnString = "FakeConnString";
            logItemInfo = new LogItem(LogLevel.INFO, "20130909 10:09:10", "This is an info");
            logItemWarning = new LogItem(LogLevel.WARNING, "20130909 10:09:10", "This is a warning");
            logItemError = new LogItem(LogLevel.ERROR, "20130909 10:09:10", "This is an error");
        }

        [Test]
        public void insertValidLogItemToOkConnString()
        {
            Model model = new Model(okConnString);
            Assert.True(model.Insert(logItemInfo));
            Assert.True(model.Insert(logItemWarning));
            Assert.True(model.Insert(logItemError));
        }

        [Test]
        public void insertValidLogItemToFakeConnString()
        {
            Model model = new Model(fakeConnString);
            Assert.False(model.Insert(logItemInfo));
            Assert.False(model.Insert(logItemWarning));
            Assert.False(model.Insert(logItemError));
        }

        [Test]
        public void insertNullLogItemToNullAndEmptyConnString()
        {
            Model nullConnModel = new Model(null);
            Model emptyConnModel = new Model("");
            Assert.False(nullConnModel.Insert(null));
            Assert.False(emptyConnModel.Insert(null));
        }

        [Test]
        public void insertNullLogItemToOkConnString()
        {
            Model okModel = new Model(okConnString);
            Assert.False(okModel.Insert(null));
        }

        [Test]
        public void insertNullLogItemToFakeConnString()
        {
            Model fakeModel = new Model(fakeConnString);
            Assert.False(fakeModel.Insert(null));
        }
    }
}
