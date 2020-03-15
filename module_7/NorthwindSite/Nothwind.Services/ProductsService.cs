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
        private readonly NorthwindContext _northwindContext;
        public ProductsService(NorthwindContext context)
        {
            _northwindContext = context;
        }

        public IEnumerable<Products> GetProducts(int page, int pageSize)
        {
            IEnumerable<Products> products = null;
            if (pageSize == 0)
            {
                products = _northwindContext
                    .Products
                    .Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .ToList();
            }

            else if (pageSize > 0)
            {
                products = _northwindContext
                    .Products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .ToList();
            }

            return products;
        }

        public int GetProductsQty()
        {
            return _northwindContext
                .Products
                .Count();
        }

        public void CreateProduct(Products product)
        {
            _northwindContext.Products.Add(product);
            _northwindContext.SaveChanges();
        }

        public Products GetProductById(int id)
        {
            return _northwindContext
                .Products
                .First(x => x.ProductId == id);
        }

        public void UpdateProduct(Products product)
        {
            _northwindContext.Entry(product).State = EntityState.Modified;
            _northwindContext.SaveChanges();
        }
    }
}
