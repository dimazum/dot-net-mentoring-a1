using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data.Models;

namespace Nothwind.Services.Interafaces
{
    public interface IProductsService
    {
        IEnumerable<Products> GetProducts(int page, int pageSize);
        int GetProductsQty();
        void CreateProduct(Products product);
    }
}
