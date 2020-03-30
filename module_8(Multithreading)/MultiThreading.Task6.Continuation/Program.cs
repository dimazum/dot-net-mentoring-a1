/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            var task = Task
                .Run(() => CreateRandomIntegers())
                .ContinueWith(x => CreateRandomIntegers())
                .ContinueWith(x => CreateRandomIntegers(), TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith(x => CreateRandomIntegers(), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously).ContinueWith(x => CreateRandomIntegers(), TaskContinuationOptions.RunContinuationsAsynchronously);
  
            task.Start();

            Console.ReadLine();
        }
        private static void CreateRandomIntegers()
        {
            Console.WriteLine("Do something...");
        }
    }
}
