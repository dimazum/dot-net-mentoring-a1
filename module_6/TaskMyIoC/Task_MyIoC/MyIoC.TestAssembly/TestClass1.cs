using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC.TestAssembly
{
    [Export(typeof(ITestClass1))]
    public class TestClass1 : ITestClass1
    {
    }

    public interface ITestClass1
    {
    }

    [ImportConstructor]
    public class TestClass2 
    {
        public TestClass2(ITestClass1 testClass1)
        {
        }

        [Import]
        public string Prop1 { get; set; }

    }
}
