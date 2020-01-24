using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FirstCharacterViewer.Interfaces;

namespace FirstCharacterViewer.Tests
{
    public class FirstCharacterTest
    {
        private IFirstCharacter _firstCharacter;

        [SetUp]
        public void Setup()
        {
            _firstCharacter = new FirstCharacter();
        }

        [Test]
        [TestCase(null)]
        public void GetFirstCharacter_Null(string str)
        {
            var expected =  _firstCharacter.GetFirstCharacter(str);

            Assert.AreEqual(expected, "");
        }
 
        [Test]
        [TestCase("abcd")]
        public void StrToInt_AreEqual(string str)
        {
            var expected = _firstCharacter.GetFirstCharacter(str);

            Assert.AreEqual(expected, "a");
        }
    }
}
