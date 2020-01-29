// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
    {
        

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine("Numbers < 5:");
			foreach (var x in lowNums)
			{
				Console.WriteLine(x);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample return return all presented in market products")]

		public void Linq2()
		{
			var products =
				from p in dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}

        [Category("Restriction Operators")]
        [Title("Where - Task 001")]
        [Description("This sample return return all presented in market products")]

        public void Linq001()
        {
            decimal[] arr = { 10000, 100000, 1000 };

            var customers = dataSource.Customers
                .Where(c => c.Orders.Sum(y => y.Total) > arr[1])
                .Select(c => new { c.CompanyName, summ = c.Orders.Sum(y => y.Total) });

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }


        [Category("Restriction Operators")]
        [Title("Where - Task 002_1")]
        [Description("This sample return return all presented in market products")]

        public void Linq002_1()
        {

            var customers = dataSource.Customers
                .Select(x => new
                {
                    x.CompanyName,
                    list = dataSource.Suppliers.Where(y => y.Country == x.Country).Select(z => z.SupplierName)
                });


            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
                foreach (var supp in p.list)
                {
                    ObjectDumper.Write(supp + ", ");
                }
                ObjectDumper.Write("--------------------------------");
            }
        }


        //[Category("Restriction Operators")]
        //[Title("Where - Task 002_2")]
        //[Description("This sample return return all presented in market products")]

        //public void Linq002_2()
        //{

        //    var customers = dataSource.Customers.Join(dataSource.Suppliers, x => x.CustomerID, y =>y.SupplierName, ) ;



        //    foreach (var p in customers)
        //    {
        //        ObjectDumper.Write(p);
        //        foreach (var supp in p.list)
        //        {
        //            ObjectDumper.Write(supp + ", ");
        //        }
        //        ObjectDumper.Write("--------------------------------");
        //    }
        //}


        [Category("Restriction Operators")]
        [Title("Where - Task 003")]
        [Description("This sample return return all presented in market products")]

        public void Linq003()
        {
            var orderValue = 1000;
            var customers = dataSource.Customers
                .Where(x => x.Orders.Any(y => y.Total > orderValue))
                .Select(x => new
                {
                    x.CompanyName,
                    orders = x.Orders.Select(o => o.Total).Where(y => y > orderValue)
                });

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
                foreach (var order in p.orders)
                {
                    ObjectDumper.Write(order);
                }
            }
        }

        [Category("Projection Operators")]
        [Title("Where - Task 004")]
        [Description("This sample return return all presented in market products")]

        public void Linq004()
        {
            var customers = dataSource.Customers.Select(x => new
            {
                x.CompanyName,
                firstOrder = x.Orders.Any() ? x.Orders.Select(y => y.OrderDate).Min() : new DateTime()
            });

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }


        [Category("Projection and sorting Operators")]
        [Title("Where - Task 005")]
        [Description("This sample return return all presented in market products")]

        public void Linq005()
        {
            var customers = dataSource.Customers
                .Select(x => new
                {
                    x.CompanyName,
                    sum = x.Orders.Sum(t => t.Total),
                    firstOrder = x.Orders.Any() ? x.Orders.Select(y => y.OrderDate).Min() : new DateTime()
                })
                .OrderBy(x => x.firstOrder.Year)
                .ThenBy(x => x.firstOrder.Month)
                .ThenByDescending(x => x.sum)
                .ThenBy(x => x.CompanyName);

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Filtering Operators")]
        [Title("Where - Task 006")]
        [Description("This sample return return all presented in market products")]

        public void Linq006()
        {
            var customers = dataSource.Customers.Where(x => !IsMatch(x.PostalCode, @"^[0-9]*$")
                                                         || !string.IsNullOrEmpty(x.Region)
                                                         || !IsMatch(x.Phone, @"^(\()"));

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Grouping Operators")]
        [Title("Where - Task 007")]
        [Description("This sample return return all presented in market products")]

        public void Linq007()
        {
            int counter = 0;
            var customers = dataSource.Products.GroupBy(x => x.Category)
                .Select((x) => new
                {
                    categoryName = x.Key,
                    products = x.GroupBy(y => y.UnitsInStock != 0 ? "YES" :"NO")
                        .Select(z => new
                        {
                            groupNumber = counter++,
                            InTheStock = z.Key,
                            products = z.Key == "YES" ? z.OrderBy(o => o.UnitPrice) : z.Select(f=>f)
                        })
                });

            foreach (var p in customers)
            {
                ObjectDumper.Write($"Category: {p.categoryName}");
                foreach (var product in p.products)
                {
                    ObjectDumper.Write($"In the stock {product.InTheStock}   groupNumber = {product.groupNumber}");
                    foreach (var prod in product.products)
                    {
                        ObjectDumper.Write($"Product :{prod.ProductName}");
                    }
                }
            }
        }

        [Category("Grouping Operators")]
        [Title("Where - Task 008")]
        [Description("This sample return return all presented in market products")]

        public void Linq008()
        {
            var customers = dataSource.Products.GroupBy(x => x.UnitPrice < 5 ? "cheap" :
                x.UnitPrice >= 10 && x.UnitPrice <= 20 ? "average" : "expensive")
                .Select(x => new
                {
                    group = x.Key,
                    products = x.Select(y => y.ProductName),

                });

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
                foreach (var product in p.products)
                {
                    ObjectDumper.Write(product);
                }
                ObjectDumper.Write("--------------------------------");
            }
        }


        [Category("Grouping Operators")]
        [Title("Where - Task 009")]
        [Description("This sample return return all presented in market products")]

        public void Linq009()
        {
            var customers = dataSource.Customers.GroupBy(x => x.City)
                .Select(x => new
                {
                    city = x.Key,
                    avgOrder = x.SelectMany(c=>c.Orders).Select(v=>v.Total).Average(),
                    avgOrderNumbers = x.SelectMany(n => n.Orders).Count() / (double)x.Select(v=>v.CustomerID).Distinct().Count(),
                });

            foreach (var p in customers)
            {
                ObjectDumper.Write(p);
            }
        }


        [Category("Grouping Operators")]
        [Title("Where - Task 010")]
        [Description("This sample return return all presented in market products")]

        public void Linq010()
        {
            var statisticsByMonth = dataSource.Customers.
                SelectMany(c => c.Orders).
                GroupBy(o => o.OrderDate.Month)
                .Select(x=> new
                {
                    x.Key,
                    countOrders = x.Select(y=>y.OrderID).Count()
                })
                .OrderBy(x=>x.Key);

            var statisticsByYear = dataSource.Customers.
                SelectMany(c => c.Orders).
                GroupBy(o => o.OrderDate.Year)
                .Select(x => new
                {
                    x.Key,
                    countOrders = x.Select(y => y.OrderID).Count()
                })
                .OrderBy(x => x.Key);

            var statisticsByYearAndMonth = dataSource.Customers.
                SelectMany(c => c.Orders).
                GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month})
                .Select(x => new
                {
                    x.Key.Year,
                    x.Key.Month,
                    countOrders = x.Select(y => y.OrderID).Count()
                })
                .OrderBy(x=>x.Year)
                .ThenBy(y=>y.Month);

            foreach (var p in statisticsByMonth)
            {
                ObjectDumper.Write(p);
            }

            foreach (var p in statisticsByYear)
            {
                ObjectDumper.Write(p);
            }

            foreach (var p in statisticsByYearAndMonth)
            {
                ObjectDumper.Write(p);
            }
        }

        private bool IsMatch(string s, string mask)
        {
            if (s == null) return false;

            return Regex.IsMatch(s, mask);
        }
    }

}
