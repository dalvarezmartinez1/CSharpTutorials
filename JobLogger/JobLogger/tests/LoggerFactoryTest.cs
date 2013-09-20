using NUnit.Framework;

namespace JobLogger.tests
{
    [TestFixture]
    public class LoggerFactoryTest
    {

        [Test]
        public void getLoggers()
        {
            Assert.True(LoggerFactory.getLoggers(LogDestination.FILE).Length == 1);
            Assert.True(LoggerFactory.getLoggers(LogDestination.DATABASE).Length == 1);
            Assert.True(LoggerFactory.getLoggers(LogDestination.CONSOLE).Length == 1);
            Assert.True(LoggerFactory.getLoggers(LogDestination.CONSOLE, LogDestination.FILE).Length == 2);
            Assert.True(LoggerFactory.getLoggers(LogDestination.CONSOLE, LogDestination.FILE, LogDestination.DATABASE, LogDestination.FILE).Length == 3);
            Assert.True(LoggerFactory.getLoggers(LogDestination.CONSOLE, LogDestination.FILE, LogDestination.DATABASE).Length == 3);
        }

        [Test]
        public void getLoggersEmptyLogDest()
        {
            LogDestination[] logDest = new LogDestination[0];
            Assert.True(LoggerFactory.getLoggers(logDest).Length == 0);
        }

        [Test]
        public void getLoggersNullLogDest()
        {
            Assert.True(LoggerFactory.getLoggers(null).Length == 0);
        }

    }
}
