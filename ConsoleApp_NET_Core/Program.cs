using System;

namespace ConsoleApp_NET_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = MyLibrary.Informer.SayHello(args[0]);
            Console.WriteLine(message);
        }
    }
}
