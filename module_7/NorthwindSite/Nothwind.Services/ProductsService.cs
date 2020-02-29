using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data.Models;
using Nothwind.Services.Interafaces;

namespace Nothwind.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IServiceProvider _serviceProvider;
        public ProductsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IEnumerable<Products> GetProducts(int page, int pageSize)
        {
            var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())

            using (var dbContext = scope.ServiceProvider.GetRequiredService<NorthwindContext>())
            {
                return dbContext
                    .Products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .ToList();
            }
        }

        public int GetProductsQty()
        {
            var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())

            using (var dbContext = scope.ServiceProvider.GetRequiredService<NorthwindContext>())
            {
                return dbContext
                    .Products
                    .Count();
            }
        }
    }
}
