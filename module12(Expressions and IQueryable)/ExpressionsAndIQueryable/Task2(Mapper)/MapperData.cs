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
        public ConcurrentDictionary<(Type, Type), ConcurrentDictionary<string, Expression>> ConfigMembers { get; set; }

        public MapperData()
        {
            ConfigMembers = new ConcurrentDictionary<(Type, Type), ConcurrentDictionary<string, Expression>>();
        }

        public ConcurrentDictionary<string, Expression> GetConfigMembers<TSource, TDest>()
        {
            var key = (typeof(TSource), typeof(TDest));
            ConcurrentDictionary<string, Expression> members;
            if (ConfigMembers.TryGetValue(key, out members))
            {
                return members;
            }

            return null;
        }
    }
}
