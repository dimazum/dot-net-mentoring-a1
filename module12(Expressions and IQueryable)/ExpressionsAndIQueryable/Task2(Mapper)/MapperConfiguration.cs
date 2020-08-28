using System;
using System.Collections.Generic;
using System.Text;
using Task2_Mapper_.Interfaces;

namespace Task2_Mapper_
{
    public class MapperConfiguration
    {
        public IMemberConfiguration<TSource, TDest> Register<TSource, TDest>()
        {
            return new MemberConfiguration<TSource, TDest>();
        }
    }
}
