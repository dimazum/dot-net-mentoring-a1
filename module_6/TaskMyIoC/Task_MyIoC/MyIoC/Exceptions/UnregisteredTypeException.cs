using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC.Exceptions
{
    public class UnregisteredTypeException : Exception
    {
        public UnregisteredTypeException(string msg):base(msg)
        {
        }
    }
}
