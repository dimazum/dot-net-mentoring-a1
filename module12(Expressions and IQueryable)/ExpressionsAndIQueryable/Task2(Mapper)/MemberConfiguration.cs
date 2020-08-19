using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Task2_Mapper_.Interfaces;

namespace Task2_Mapper_
{
    public class MemberConfiguration<TSource, TDest> : IMemberConfiguration<TSource, TDest>
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
             ConcurrentDictionary<string, Expression> members;
            if (!configMembers.TryGetValue(key, out members))
            {
                members = new ConcurrentDictionary<string, Expression>();
                members.TryAdd(ConvertExpressionToPropertyName(src), dest);
                configMembers.TryAdd(key, members);
            }
            else
            {
                members.TryAdd(ConvertExpressionToPropertyName(src), dest);

                ConcurrentDictionary<string, Expression> newMembers;
                configMembers.TryGetValue(key, out newMembers);
                configMembers.TryUpdate(key, members, newMembers);
            }
            return this;
        }

        private string ConvertExpressionToPropertyName(Expression expression)
        {
            var type = (expression as LambdaExpression)?.Body;
            var propertyInfo = ((MemberExpression)type).Member as PropertyInfo;

            return propertyInfo?.Name;
        }
    }
}
