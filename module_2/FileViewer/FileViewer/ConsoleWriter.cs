using System;
using System.Collections.Generic;
using System.Text;
using BLL.Implementation;
using BLL.Interfaces;
using DAL.Interfaces;

namespace FileViewer
{
    public class ConsoleWriter
    {
        public void ConsoleOutputEnumerable(IEnumerable<string> enumerable)
        {
            foreach (var item in enumerable)
            {
                Console.WriteLine(item);
            }
        }

        public void ConsoleOutputString(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
