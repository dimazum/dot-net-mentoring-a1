using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_Mapper_
{
    public class MapperConfiguration
    {
        public MemberConfiguration<TSource, TDest> Register<TSource, TDest>()
        {
            return new MemberConfiguration<TSource, TDest>();
        }
    }
}
