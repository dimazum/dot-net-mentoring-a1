using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        private readonly ICategoriesService _categoriesService;
        private readonly IProductsService _productsService;
        private static int _currentPage; //???

        public HomeController(ILogger<HomeController> logger, ICategoriesService categoriesService, IProductsService productsService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _productsService = productsService;
        }

        public IActionResult Index()
        {
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

        public IActionResult Products( int page = 1)
        {
           var productsPageViewModel  = CreateProductsPageViewModel(page);

            return View(productsPageViewModel);
        }

        private ProductsPageViewModel CreateProductsPageViewModel(int page , ProductViewModel productViewModel = null)
        {
            _currentPage = page;
            var pageSize = 10;

            ProductViewModel _productViewModel = null;

            if (productViewModel != null)
            {
                productViewModel.Categories = _categoriesService.GetCategories();
                productViewModel.Suppliers = _categoriesService.GetSuppliers();
                productViewModel.PopupOn = true;
            }
            else
            {
                _productViewModel = new ProductViewModel()
                {
                    Categories = _categoriesService.GetCategories(),
                    Suppliers = _categoriesService.GetSuppliers(),
                };
            }

            var products = _productsService.GetProducts(page, pageSize);
            var productQty = _productsService.GetProductsQty();
            var paginationInfo = new PaginationInfo(productQty, page, pageSize);

            var productsPageViewModel = new ProductsPageViewModel()
            {
                Products = products,
                PaginationInfo = paginationInfo,
                ProductViewModel = productViewModel ?? _productViewModel
            };

            return productsPageViewModel;
        }

        public IActionResult CreateProduct(ProductViewModel productViewModel)
        {

            if (productViewModel.ProductName?.Length < 10)
            {
                ModelState.AddModelError(nameof(productViewModel.ProductName), "Invalid string length");
            }

            if (!ModelState.IsValid)
            {
                var productsPageViewModel = CreateProductsPageViewModel(_currentPage, productViewModel);

                return View("Products", productsPageViewModel);
            }

            var category = _categoriesService.GetCategoryByName(productViewModel.Category);
            var supplier = _categoriesService.GetSupplierByName(productViewModel.Supplier);

            var product = new Products
            {
                ProductName = productViewModel.ProductName,
                QuantityPerUnit = productViewModel.QuantityPerUnit,
                UnitPrice = productViewModel.UnitPrice,
                CategoryId = category.CategoryId,
                SupplierId = supplier.SupplierId
            };
            _productsService.CreateProduct(product);

            return RedirectToAction("Products");
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
