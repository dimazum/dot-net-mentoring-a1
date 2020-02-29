using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.Data.Models;
using NorthwindSite.Models;
using Nothwind.Services.Interafaces;

namespace NorthwindSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICategoriesService _categoriesService;
        private readonly IProductsService _productsService;

        public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider, ICategoriesService categoriesService, IProductsService productsService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _categoriesService = categoriesService;
            _productsService = productsService;
        }

        public IActionResult Index()
        {
            var msg = _categoriesService.GetCategories();

            return View();
        }

        public IActionResult Categories()
        {
            var categories = _categoriesService.GetCategories();
            var categoriesViewModel = new CategoriesPageViewModel()
            {
                Categories = categories
            };
            return View(categoriesViewModel);
        }

        public IActionResult Products(int page = 1)
        {
            var pageSize = 10;
            var products = _productsService.GetProducts(page, pageSize);
            var productQty = _productsService.GetProductsQty();
            var paginationInfo = new PaginationInfo(productQty, page, pageSize);

            var categoriesViewModel = new ProductsPageViewModel()
            {
                Products = products,
                PaginationInfo = paginationInfo
            };
            return View(categoriesViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
