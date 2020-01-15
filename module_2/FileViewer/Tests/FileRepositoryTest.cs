using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BLL.Implementation;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.Common.Filtering;

namespace Tests
{
    public class FileRepositoryTest
    {
        private IFileRepository _fileRepository;
        [SetUp]
        public void Setup()
        {
            var cofigurationMock = new Mock<IConfiguration>();
            cofigurationMock.SetupGet(x => x[It.IsAny<string>()]).Returns(@"C:\Users\Dzmitry_Khileuski\Desktop\temp\1111");
            _fileRepository = new FileRepository(cofigurationMock.Object);

        }

        static IEnumerable TestData()
        {
            yield return new TestCaseData(new Func<string, bool>(x => x == "22"));
        }
        static IEnumerable TestData2()
        {
            yield return new TestCaseData(new Func<string, bool>(x => x == "11"));
        }

        static IEnumerable TestData3()
        {
            yield return new TestCaseData(new Func<string, bool>(x => x == "211.txt"));
        }

        [Test]
        [TestCase(null)]
        [TestCaseSource(nameof(TestData))]
        [TestCaseSource(nameof(TestData2))]
        public void GetDirectories_Null(Func<string,bool> func)
        {
            var result = _fileRepository.GetDirectories(func);
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(TestData2))]
        public void GetDirectories_Value__Equal(Func<string, bool> func)
        {
            var result = _fileRepository.GetDirectories(func);
            var expected = @"C:\Users\Dzmitry_Khileuski\Desktop\temp\1111\22\222\11";
            Assert.AreEqual(expected,result.First());
        }

        [Test]
        [TestCase(null)]
        [TestCaseSource(nameof(TestData))]
        [TestCaseSource(nameof(TestData2))]
        public void GetFiles_Null(Func<string, bool> func)
        {
            var result = _fileRepository.GetFiles(func);
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(TestData3))]
        public void GetFiles_Value__Equal(Func<string, bool> func)
        {
            var result = _fileRepository.GetFiles(func);
            var expected = @"C:\Users\Dzmitry_Khileuski\Desktop\temp\1111\22\222\211.txt";
            Assert.AreEqual(expected, result.First());
        }
    }
}