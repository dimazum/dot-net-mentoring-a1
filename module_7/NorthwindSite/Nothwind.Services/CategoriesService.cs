using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data;
using Northwind.Data.Models;
using Nothwind.Services.Interafaces;

namespace Nothwind.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IContextFactory _contextFactory;
        public CategoriesService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<Categories> GetCategories()
        {

            using (var dbContext = _contextFactory.Create<NorthwindContext>() )
            {
                return dbContext.Categories.ToList();
            }
        }

        public Categories GetCategoryByName(string name)
        {
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
            {
                return dbContext
                    .Categories
                    .First(x => x.CategoryName == name);
            }
        }

        public IEnumerable<Suppliers> GetSuppliers()
        {
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
            {
                return dbContext.Suppliers.ToList();
            }
        }

        public Suppliers GetSupplierByName(string name)
        {
            using (var dbContext = _contextFactory.Create<NorthwindContext>())
            {
                return dbContext
                    .Suppliers
                    .First(x => x.CompanyName == name);
            }
        }
    }
}
