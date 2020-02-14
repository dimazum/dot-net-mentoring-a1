using System.IO;
using ConsoleApp1;
using ConsoleApp1.Configuration;
using Moq;
using NUnit.Framework;

namespace FileSorter.Tests
{
    public class Tests
    {
        private FileWatcher fileWatcher;
        [SetUp]
        public void Setup()
        {
            var configMock = new Mock<WatcherConfigurationSection>();
            configMock.SetupGet(x => x.CultureInfo.Culture).Returns("ru-Ru");

            fileWatcher = new FileWatcher(configMock.Object);

        }
    }
}