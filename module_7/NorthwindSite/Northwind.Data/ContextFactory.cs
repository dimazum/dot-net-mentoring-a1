using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data.Models;

namespace Northwind.Data
{
    public class ContextFactory : IContextFactory
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public ContextFactory(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public T Create<T>()
        {
            var scope = _scopeFactory.CreateScope();
            return scope.ServiceProvider.GetService<T>();
        }
    }

    public interface IContextFactory
    {
        T Create<T>();
    }
}
