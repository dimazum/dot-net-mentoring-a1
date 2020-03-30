/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static Semaphore sem = new Semaphore(1,1);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code

            Console.WriteLine($"The Main thread. ThreadId = {Thread.CurrentThread.ManagedThreadId}");

            var count = 10;
           // ThreadCreate(count);
            ThreadPoolCreate(count);

            Console.ReadLine();
        }

        public static void ThreadCreate(int state)
        {
            int count = state;

            if (count >= 0)
            {
                Console.WriteLine($"Count = {count}. ThreadId = {Thread.CurrentThread.ManagedThreadId}");
                count--;

                Thread thread = new Thread(() => ThreadCreate(count));
                thread.Start();
                thread.Join();
            }
            else
            {
                Console.WriteLine("Counter is less then zero");
            }
        }

        public static void ThreadPoolCreate(int state)
        {
            int count = state;

            ThreadPool.QueueUserWorkItem(x =>
            {
                count--;
                Console.WriteLine($"Count = {count}. ThreadId = {Thread.CurrentThread.ManagedThreadId}");
                
                if (count <= 0)
                {
                    Console.WriteLine("Counter is less then zero.");
                    return;
                }
                ThreadPoolCreate(count);
            });

            if (count == 10)
            {
                sem.WaitOne();
            }

            else if (count == 1)
            {
                sem.Release();
            }
        }
    }
}
