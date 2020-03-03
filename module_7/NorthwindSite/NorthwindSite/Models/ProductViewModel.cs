using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindSite.Models
{
    public class ProductViewModel
    {
        [Required]
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        public decimal? UnitPrice { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public bool PopupOn { get; set; }

        public virtual IEnumerable<Categories> Categories { get; set; }
        public virtual IEnumerable<Suppliers> Suppliers { get; set; }
    }
}
