using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Task2_Mapper_.Models;

namespace Task2_Mapper_
{
    public class MappingGenerator
    {
        private readonly MapperData _mapperData;

        public MappingGenerator()
        {
            _mapperData = Singleton<MapperData>.Instance;
        }

        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var constructorInfoForDestinationType = destinationType.GetConstructors().First();

            ParameterExpression sourceParam = Expression.Parameter(typeof(TSource), "sParam");
            var ctor = Expression.New(destinationType);
            var body = Expression.MemberInit(ctor, GetMemberBindings<TSource, TDestination>(sourceParam));


            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }

        public IEnumerable<MemberBinding> GetMemberBindings<TSource, TDest>(ParameterExpression sourceParam)
        {
            //ParameterExpression sourceParam = Expression.Parameter(typeof(TSource), "sParam");
            //var bindings = typeof(TSource).GetProperties()
            //   .Select(x => 
            //   Expression.Bind(typeof(TDest).GetProperty(x.Name), Expression.Property(sourceParam, x)));

            //return bindings;

            var memberConfigs = _mapperData.GetConfigMembers<TSource, TDest>();

            //Func<Student, bool> isTeenAger = memberConfigs.Keys[0].Compile();
            var ss = memberConfigs.Keys.First();
            var sss = ((Expression<Func<TSource, string>>)ss);
            var ssss = ((MemberExpression)sss.Body).Member as PropertyInfo;

            var ss1 = memberConfigs.Values.First();
            var sss1 = ((Expression<Func<TDest, string>>)ss1);

            if (sss1.Body is MemberExpression)
            {
                var ssss1 = ((MemberExpression)sss1.Body).Member as PropertyInfo;
            }
            else if (sss1.Body is ConstantExpression)
            {
                var sssss = ((ConstantExpression)sss1.Body).Value as PropertyInfo;
            }

            var list = new List<MemberBinding>();

            foreach (var prop in typeof(TSource).GetProperties())
            {

                yield return Expression
                    .Bind(typeof(TDest)
                        .GetProperty(prop.Name), Expression.Property(sourceParam, prop));
            }
        }

        //public void ConvertExpression<TSource, TDest>(string propName)
        //{
        //    var memberConfigs = _mapperData.GetConfigMembers<TSource, TDest>();
        //    var exp = (Expression<Func<TSource, string>>)expression;
        //    var propertyInfo = ((MemberExpression)exp.Body).Member as PropertyInfo;

        //    return propertyInfo?.Name;
        //}
    }

    class Student
    {
        public int age;
    }
}

