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
        private readonly NorthwindContext _northwindContext;
        public CategoriesService(NorthwindContext context)
        {
            _northwindContext = context;
        }
        public IEnumerable<Categories> GetCategories()
        {
            return _northwindContext.Categories.ToList();
        }

        public Categories GetCategoryByName(string name)
        {
            return _northwindContext
                .Categories
                .First(x => x.CategoryName == name);
        }

        public IEnumerable<Suppliers> GetSuppliers()
        {
            return _northwindContext.Suppliers.ToList();
        }

        public Suppliers GetSupplierByName(string name)
        {

            return _northwindContext
                .Suppliers
                .First(x => x.CompanyName == name);
        }
    }
}
