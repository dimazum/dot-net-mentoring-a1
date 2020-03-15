using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data.Models;

namespace Nothwind.Services.Interafaces
{
    public interface ICategoriesService
    {
        IEnumerable<Categories> GetCategories();
        IEnumerable<Suppliers> GetSuppliers();
        Categories GetCategoryByName(string name);
        Suppliers GetSupplierByName(string name);
    }
}
