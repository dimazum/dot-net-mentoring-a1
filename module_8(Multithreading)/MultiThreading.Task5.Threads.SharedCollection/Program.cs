/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static Semaphore sem = new Semaphore(0, 1);
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var list = new List<int>();
            BlockingCollection<int> list2 = new BlockingCollection<int>();
            var resetEvent1 = new AutoResetEvent(false);
            var resetEvent2 = new AutoResetEvent(false);

            Semaphore sem = new Semaphore(0,1);

            var thread1 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i != 0)
                    {
                        //resetEvent1.WaitOne();
                    }
                    Thread.Sleep(100);
    
                    list.Add(i);
                    sem.Release();
                    //resetEvent2.Set();
                }
    
            });

            var thread2 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    //  resetEvent2.WaitOne();
                    sem.WaitOne();
                    list.ForEach(x=>Console.Write(x));
                    Console.WriteLine();
                   // resetEvent1.Set();
                }
               
            });

            thread1.Start();
            thread2.Start();

            Console.ReadLine();
        }
    }
}
