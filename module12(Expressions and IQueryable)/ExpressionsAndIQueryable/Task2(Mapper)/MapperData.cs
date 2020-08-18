using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Task2_Mapper_
{
    public class MapperData
    {
        public ConcurrentDictionary<(Type, Type), ConcurrentDictionary<Expression, Expression>> ConfigMembers { get; set; }

        public MapperData()
        {
            ConfigMembers = new ConcurrentDictionary<(Type, Type), ConcurrentDictionary<Expression, Expression>>();
        }

        public ConcurrentDictionary<Expression, Expression> GetConfigMembers<TSource, TDest>()
        {
            var key = (typeof(TSource), typeof(TDest));
            ConcurrentDictionary<Expression, Expression> members;
            if (ConfigMembers.TryGetValue(key, out members))
            {
                return members;
            }

            return null;
        }
    }
}
