using EFModule.Data.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace EFModule
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthwindContext())
            {
                var orders = context
                    .Orders
                    .Include(x => x.Customer)
                    .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                    .Where(x => x.OrderDetails.Any(c => c.Product.Category.CategoryName == "Beverages"))
                    .Select(x => new {
                        details = x.OrderDetails,
                        CustomerName =  x.Customer.CompanyName,
                        productNames = x.OrderDetails.Select(y => y.Product.ProductName) })
                    ;
      
            }
        }
    }
}
