using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens
{

    public class Calculator
    {
        public event Action<int, long> Counter;
        //todo: change this method to support cancellation token

        public Task<Info> Calculate(int n, CancellationToken token)
        {
            return Task<Info>.Run(() =>
            {
                long sum = 0;

                for (int i = 1; i <= n; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        return new Info { sum = sum, count = i - 1 };
                    }
                    Thread.Sleep(500);
                    sum += i;
                    Counter?.Invoke(i, sum);
                }

                return new Info { sum = sum, count = n };
            }, token);

        }
    }

    public struct Info
    {
        public long sum;
        public int count;
    }
}
