using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Data.Models;
using Nothwind.Services.Interafaces;

namespace Nothwind.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IServiceProvider _serviceProvider;
        public CategoriesService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IEnumerable<Categories> GetCategories()
        {
            using (var dbContext = _serviceProvider.GetRequiredService<NorthwindContext>() )
            {
                return dbContext.Categories.ToList();
            }
        }
    }
}
