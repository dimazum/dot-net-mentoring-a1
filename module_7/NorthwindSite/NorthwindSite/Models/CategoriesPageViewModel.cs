using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Data.Models;

namespace NorthwindSite.Models
{
    public class CategoriesPageViewModel
    {
        public IEnumerable<Categories> categories { get; set; }
    }
}
