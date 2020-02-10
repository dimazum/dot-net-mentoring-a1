using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.Configuration;
using ConsoleApp1.Resources;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        { 
 
            var fileWatcher = new FileWatcher();
            fileWatcher.StartWatching();

            Console.ReadLine();
          

        }
   }
}
