using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Task2_Mapper_.Interfaces
{
    public interface IMemberConfiguration<TSource, TDest>
    {
        MemberConfiguration<TSource, TDest> Member<T, TN>(Expression<Func<TSource, T>> src,
            Expression<Func<TDest, TN>> dest);
    }
}
