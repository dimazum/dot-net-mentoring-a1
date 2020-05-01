/*
 * Изучите код данного приложения для расчета суммы целых чисел от 0 до N, а затем
 * измените код приложения таким образом, чтобы выполнялись следующие требования:
 * 1. Расчет должен производиться асинхронно.
 * 2. N задается пользователем из консоли. Пользователь вправе внести новую границу в процессе вычислений,
 * что должно привести к перезапуску расчета.
 * 3. При перезапуске расчета приложение должно продолжить работу без каких-либо сбоев.
 */

using System;
using System.Threading;

namespace AsyncAwait.Task1.CancellationTokens
{
    class Program
    {
        /// <summary>
        /// The Main method should not be changed at all.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
            Console.WriteLine("Calculating the sum of integers from 0 to N.");
            Console.WriteLine("Use 'q' key to exit...");
            Console.WriteLine();

            Console.WriteLine("Enter N: ");

            string input = Console.ReadLine();

            while (input.Trim().ToUpper() != "Q")
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                CancellationToken token = cancelTokenSource.Token;
                var isCanceled1 = token.CanBeCanceled;

                var token2 = CancellationToken.None;
                var isCanceled2 = token2.CanBeCanceled;
                if (int.TryParse(input, out int n))
                {
                    CalculateSum(n, token);
                }
                else
                {
                    Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
                    Console.WriteLine("Enter N: ");
                }

                input = Console.ReadLine();
                cancelTokenSource.Cancel();
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static void Calculator_Counter(int count, long sum)
        {
            Console.WriteLine($"Count: {count}, summ: {sum}");
        }

        private static void CalculateSum(int n, CancellationToken token)
        {
            var calc = new Calculator();
            calc.Counter += Calculator_Counter;

            var sum = calc.Calculate(n, token);
            Console.WriteLine($"The task for {n} started... Enter N to cancel the request:");

            sum.ContinueWith(x =>
            {
                Console.WriteLine("============================");
                Console.WriteLine($"Sum for {x.Result.count} = {x.Result.sum}.");
                Console.WriteLine("============================");
            });

            Console.WriteLine();
            Console.WriteLine("Enter N: ");
        }
    }
}