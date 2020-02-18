using System;
using System.Collections.Generic;
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
        [SetUp]
        public void Setup()
        {
            _container = new Container();
            _registeredTypesDictionary = new Dictionary<Type, Type>();
        }

        [Test]
        public void Test1()
        {
            var assembly = Assembly.Load("MyIoC.TestAssembly");
            //var assembly = Assembly.GetExecutingAssembly();
            foreach (var typeInfo in assembly.DefinedTypes)
            {
                foreach (ExportAttribute exportAttribute in typeInfo.GetCustomAttributes<ExportAttribute>())
                {
                    if (exportAttribute.Contract != null)
                        _registeredTypesDictionary.Add(exportAttribute.Contract, typeInfo);
                    else
                        _registeredTypesDictionary.Add(typeInfo, typeInfo);
                }
            }

            //_container.CreateInstance()

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