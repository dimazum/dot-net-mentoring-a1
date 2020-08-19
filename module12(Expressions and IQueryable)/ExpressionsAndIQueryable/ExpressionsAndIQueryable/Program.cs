using MyExpressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using ExpressMapper;
//using ExpressMapper;
//using ExpressMapper.Extensions;
using Task2_Mapper_;
using Task2_Mapper_.Models;

namespace ExpressionsAndIQueryable
{
    class Program
    {
        static void Main(string[] args)
        {
            //int n = 15;

            //Expression<Func<int, int>> initialExpression = x => x * 2 + (x + 1) + (1 + x) + 10 + (x - 1) - (1 - x);
            //var addToIncrementTransformer = new IncrementAndDecrementTransform();
            //var transformedExpression = addToIncrementTransformer.VisitAndConvert(initialExpression, "");

            //Console.WriteLine($"Expression: {initialExpression}\nTransformed expression: {transformedExpression}\nInvoke with number = {n}. Result = {transformedExpression.Compile().Invoke(n)}\n\n");





            //var dictionary = new Dictionary<string, int>
            //{
            //    {"x", 10},
            //    {"y", 20},
            //    {"z", 30}
            //};


            //Expression<Func<int, int, int, double>> initExpression = (x, y, z) => x * 2 + (y + 1) + (1 + z);
            //var parameterTransformer = new ParameterToConstantTransformer(dictionary);
            //var transformed = parameterTransformer.VisitAndConvert(initExpression, "");

            //Console.WriteLine($"Initial expression: {initExpression}\nTransformed expression: {transformed}\nResult = {transformed.Compile().Invoke(0, 0, 0)}");


            //Mapper.Register<Foo, Bar>().Member(y => "fefefe", x=>x.Name);
            //--------
            var foo = new Foo
            {
                Id = 1,
                Name = "Dzmitry",
                Age = 333,
                Email = "test@gmail.com"
            };

            var mapConfig = new MapperConfiguration()
                .Register<Foo, Bar>()
                .Member(x => x.Name, y => "Dima2")
                .Member(x => x.Name, y => y.Name)
                .Member(x => x.Id, y => y.Id2)
                .Member(x => x.Id, y => y.Id2);
                //.Member(x => x.Id, y => 43434);


            var mappingGenerator = new MappingGenerator();
            var mapper = mappingGenerator.Generate<Foo, Bar>();

            var result = mapper.Map(foo);


        }

    }
}
