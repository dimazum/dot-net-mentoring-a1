using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            var destinationType = typeof(TDestination);

            var sourceParam = Expression.Parameter(typeof(TSource), "sParam");
            var ctor = Expression.New(destinationType);
            var memberBindings = GetMemberBindings<TSource, TDestination>(sourceParam);
            var body = Expression.MemberInit(ctor, memberBindings);

            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }

        public IEnumerable<MemberBinding> GetMemberBindings<TSource, TDest>(ParameterExpression sourceParam)
        {
            foreach (var prop in typeof(TSource).GetProperties())
            {
                var configBind = GetExpression<TSource, TDest>(prop.Name);
                if (configBind == null)
                {
 
                    yield return BindByDefault<TDest>(sourceParam, prop);
                }
                else
                {
                    var typeBody = (configBind as LambdaExpression)?.Body;
                    if (typeBody is MemberExpression)
                    {
                        var propertyInfo = ((MemberExpression)typeBody).Member as PropertyInfo;
                        if (propertyInfo != null)
                        {
                            yield return Expression
                                .Bind(propertyInfo, Expression.Property(sourceParam, prop));
                        }
                    }
                    else if (typeBody is ConstantExpression)
                    {
                        var constant = ((ConstantExpression) typeBody).Value;
                        var memberInfo = typeof(TDest).GetMember(prop.Name).FirstOrDefault();
                        if (memberInfo == null)
                        {
                            throw new Exception($"Cannot bind the property. Property: {prop.Name}");
                        }

                        yield return Expression
                            .Bind(memberInfo, Expression.Constant(constant));
                    }
                }
            }
        }

        private MemberAssignment BindByDefault<TDest>(ParameterExpression sourceParam, PropertyInfo propertyInfo)
        {
            var propInfo = typeof(TDest).GetProperty(propertyInfo.Name);

            if (propInfo == null)
            {
                throw new Exception($"Cannot bind the property. Property: {propertyInfo.Name}");
            }
            
            var memberAssignment = Expression
                    .Bind(propInfo, Expression.Property(sourceParam, propertyInfo));
            

            return memberAssignment;
        }

        private Expression GetExpression<TSource, TDest>(string propName)
        {
            var memberConfigs = _mapperData.GetConfigMembers<TSource, TDest>();
            Expression expression; 
            memberConfigs.TryGetValue(propName, out expression);
            return expression;
        }
    }

}
//https://entityframework-plus.net/ru/knowledge-base/47468652/
//https://docs.microsoft.com/en-us/dotnet/api/system.linq.expressions.memberinitexpression?view=netcore-3.1
