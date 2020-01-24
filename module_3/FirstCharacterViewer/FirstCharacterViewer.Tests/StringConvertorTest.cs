using System;
using NUnit.Framework;
using TypeConverter;
using TypeConverter.Interfaces;

namespace FirstCharacterViewer.Tests
{
    public class StringConvertorTest
    {
        private IStringConverter _stringConverter;
        [SetUp]
        public void Setup()
        {
            _stringConverter = new StringConverter();
        }

        [Test]
        [TestCase(null)]
        public void StrToInt_ArgumentNullException(string str)
        {
            Assert.Throws<ArgumentNullException>(() => _stringConverter.StrToInt(str));
        }

        [Test]
        [TestCase("abcd123")]
        public void StrToInt_ArgumentException(string str)
        {
            Assert.Throws<ArgumentException>(() => _stringConverter.StrToInt(str));
        }

        [Test]
        [TestCase("123")]
        public void StrToInt_AreEqual(string str)
        {
            var expected = _stringConverter.StrToInt(str);

            Assert.AreEqual(expected, 123);
        }
    }
}