
using LogTest;
using Moq;
using NUnit.Framework;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace LogComponent.UnitTests
{
	[TestFixture]

    public class LogComponentTests
	{
        /// <summary>
        /// A call to LogInterface will end up in writing something
        /// </summary>
        [Test]
        public void Create_Logger_Success()
        {
            Mock<AsyncLogger> mockLogger = new Mock<AsyncLogger>();
            Xunit.Assert.NotNull(mockLogger.Object);
        }

        /// <summary>
        /// New files are created if midnight is crossed
        /// </summary>
        [Test]
        [TestCase(2015, 2, 23)]
        [TestCase(2015, 12, 3)]
        public void Is_Files_Created_In_Midnight()
        {
            var mockFileSystem = new MockFileSystem();

            var mockInputFile = new MockFileData(@"Timestamp                	Data           	
                                                    2021 - 05 - 11 21:18:20:969 Number with Flush: 0.");

            Mock<AsyncLogger> mockLogger = new Mock<AsyncLogger>();
            mockFileSystem.AddFile(mockLogger.Object.defaultLogDirectory, mockInputFile);
            //deal with files, dates, etc
            mockLogger.Object.Write("Test");
            Xunit.Assert.True(mockFileSystem.Directory.Exists(mockLogger.Object.LogsFolder));
        }

        /// <summary>
        /// The stop behavior is working as described above
        /// </summary>
        [Test]
        public void Error_Recover_Success_StopWithFlush(string testString)
        {
            Mock<AsyncLogger> mockLogger = new Mock<AsyncLogger>();
            mockLogger.Object.StopWithFlush();
        }

        /// <summary>
        /// The stop behavior is working as described above
        /// </summary>
        [Test]
        public void Error_Recover_Success_StopWithoutFlush(string testString)
        {
            Mock<AsyncLogger> mockLogger = new Mock<AsyncLogger>();
            mockLogger.Object.StopWithoutFlush();
        }


    }
}
