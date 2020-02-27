using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyIoC.Exceptions;
using MyIoC.TestAssembly;
using NUnit.Framework;

namespace MyIoC.Tests
{
    public class Tests
    {
        private Container _container;
        private  Dictionary<Type, Type> _registeredTypesDictionary;
        private Dictionary<Type, Type> _registeredTypesDictionary2 = new Dictionary<Type, Type>()
        {
            { typeof(ITestClass1), typeof(TestClass1)}
        };
        private Assembly _assembly;
        [SetUp]
        public void Setup()
        {
            _container = new Container();
            _registeredTypesDictionary = new Dictionary<Type, Type>()
            {
                { typeof(ITestClass1), typeof(TestClass1)}
            };

            _assembly = Assembly.Load("MyIoC.TestAssembly");
        }

        [Test]
        public void AddAssembly_DictionariesAreEqual()
        {
            _container.AddAssembly(_assembly);

            var actual = _container.GetRegisteredTypes();

            var isEqual = _registeredTypesDictionary.OrderBy(pair => pair.Key)
                .SequenceEqual(actual.OrderBy(pair => pair.Key));

            Assert.IsTrue(isEqual);

        }

        [Test]
        public void CreateInstance_NotNull()
        {
            _container.AddType(typeof(TestClass1));
            var instance = _container.CreateInstance(typeof(TestClass1));

            Assert.IsNotNull(instance);
        }

        [Test]
        public void CreateInstanceGeneric_NotNull()
        {
            _container.AddType(typeof(TestClass1));
            var instance = _container.CreateInstance<TestClass1>();

            Assert.IsNotNull(instance);
        }

        [Test]
        public void CreateInstance_EqualTypes()
        {
            _container.AddType(typeof(TestClass1));
            var instance = _container.CreateInstance(typeof(TestClass1));

            var expected = new TestClass1();
            Assert.AreEqual(expected.GetType(), instance.GetType());
        }

        [Test]
        public void CreateInstance_UnregisteredTypeException()
        {
            Assert.Throws<UnregisteredTypeException>(()=>_container.CreateInstance(typeof(TestClass1)));
        }

        [Test]
        public void CreateInstance_TwoRegistrationWaysException()
        {
            _container.AddType(typeof(TestClass2));
            try
            {
                 _container.CreateInstance(typeof(TestClass2));

                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("More than one type registration way", ex.Message);
            }
        }
    }

    [Export(typeof(ITestClass1))]
    public class TestClass1 : ITestClass1
    {
    }

    public interface ITestClass1
    {
    }
}