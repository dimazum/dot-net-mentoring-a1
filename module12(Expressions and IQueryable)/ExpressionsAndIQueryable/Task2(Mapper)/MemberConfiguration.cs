using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Task2_Mapper_
{
    public class MemberConfiguration<TSource, TDest>
    {
        private readonly MapperData _mapperData;
        public MemberConfiguration()
        {
            _mapperData = Singleton<MapperData>.Instance;
        }


        public MemberConfiguration<TSource, TDest> Member<T, TN>(Expression<Func<TSource, T>> src, Expression<Func<TDest, TN>> dest)
        {
            var configMembers = _mapperData.ConfigMembers;

            var key = (typeof(TSource), typeof(TDest));
             ConcurrentDictionary<Expression, Expression> members;
            if (!configMembers.TryGetValue(key, out members))
            {
                members = new ConcurrentDictionary<Expression, Expression>();
                members.TryAdd(src, dest);
                configMembers.TryAdd(key, members);

            }
            else
            {
                members.TryAdd(src, dest);

                ConcurrentDictionary<Expression, Expression> newMembers;
                configMembers.TryGetValue(key, out newMembers);
                configMembers.TryUpdate(key, members, newMembers);
            }

            return this;
        }

        private string ConvertExpressionToPropertyName(Expression expression)
        {
            var exp = (Expression<Func<TSource, string>>)expression;
            var propertyInfo = ((MemberExpression)exp.Body).Member as PropertyInfo;

            return propertyInfo?.Name;
        }
    }
}
