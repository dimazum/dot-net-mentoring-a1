using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Data.Models;

namespace NorthwindSite.Models
{
    public class ProductsPageViewModel
    {
        public IEnumerable<Products> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
    }
}
