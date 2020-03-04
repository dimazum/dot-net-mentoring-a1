using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data;
using Northwind.Data.Models;
using Nothwind.Services.Interafaces;

namespace Nothwind.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IContextFactory _contextFactory;
        public ProductsService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Products> GetProducts(int page, int pageSize)
        {
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
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
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
            {
                return dbContext
                    .Products
                    .Count();
            }
        }

        public void CreateProduct(Products product)
        {
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
        }

        public Products GetProductById(int id)
        {
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
            {
                return dbContext
                    .Products
                    .First(x => x.ProductId == id);
            }
        }

        public void UpdateProduct(Products product)
        {
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
            {
               dbContext.Entry(product).State = EntityState.Modified;
               dbContext.SaveChanges();
            }
        }
    }
}
