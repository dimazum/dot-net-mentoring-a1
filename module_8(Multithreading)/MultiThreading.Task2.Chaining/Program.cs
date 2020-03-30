/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var task = Task
                .Run(() => CreateRandomIntegers(10))
                .ContinueWith(x => MultiplyArray(x.Result))
                .ContinueWith(x => SortArray(x.Result))
                .ContinueWith(x => GetAverageValue(x.Result))
                .Result;

            Console.WriteLine($"Average : {task}");

            Console.ReadLine();
        }

        private static int[] CreateRandomIntegers(int size)
        {
            int[] arr = new int[size];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Random().Next(1, 10);
            }

            OutputArray(arr);

            return arr;
        }

        private static int[] MultiplyArray(int[] arr)
        {
            var _arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                _arr[i] = arr[i] * new Random().Next(1, 10);
            }

            OutputArray(_arr);

            return _arr;
        }

        private static int[] SortArray(int[] arr)
        {
            Array.Sort(arr);
            OutputArray(arr);
            return arr;
        }

        private static double GetAverageValue(int[] arr)
        {
            return  arr.Average();
        }

        private static void OutputArray<T>(IEnumerable<T> arr)
        {
            foreach (var i in arr)
            {
                Console.Write($"{i}, ");
            }

            Console.WriteLine();
        }
    }
}
