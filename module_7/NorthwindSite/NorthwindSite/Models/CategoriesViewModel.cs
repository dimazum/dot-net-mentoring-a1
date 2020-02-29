using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Data.Models;

namespace NorthwindSite.Models
{
    public class CategoriesViewModel 
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public CategoriesViewModel(Categories categories)
        {
            CategoryName = categories.CategoryName;
            Description = categories.Description;
        }
    }
}
