using MyExpressions;
using System;
using System.Linq.Expressions;

namespace ExpressionsAndIQueryable
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 15;

            Expression<Func<int, int>> initialExpression = x => x * 2 + (x + 1) + (1 + x) + 10 + (x - 1);
            var addToIncrementTransformer = new IncrementAndDecrementTransform();
            var transformedExpression = addToIncrementTransformer.VisitAndConvert(initialExpression, "");

            Console.WriteLine($"Expression: {initialExpression}\nTransformed expression: {transformedExpression}\nInvoke with number = {n}. Result = {transformedExpression.Compile().Invoke(n)}\n\n");

        }
    }
}
