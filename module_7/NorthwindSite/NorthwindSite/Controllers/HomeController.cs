using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
         
        public HomeController(ILogger<HomeController> logger, ICategoriesService categoriesService, IProductsService productsService, IConfiguration configuration)
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _productsService = productsService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;


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

        private int ConvertStrToInt(string str)
        {
            int.TryParse(str, out var number);

             return number;
        }

        private ProductsPageViewModel CreateProductsPageViewModel(int page , ProductViewModel productViewModel = null)
        {
            var pageSize = ConvertStrToInt(_configuration["PageSize"]);
            
            _logger.LogInformation("Read configuration {0}",pageSize);

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

            if (productViewModel.ProductName?.Length < 2)
            {
                ModelState.AddModelError(nameof(productViewModel.ProductName), "Invalid string length");
            }

            if (!ModelState.IsValid)
            {
                var productsPageViewModel = CreateProductsPageViewModel(productViewModel.CurrentPage, productViewModel);

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

        public IActionResult UpdateProduct(ProductViewModel productViewModel)
        {
            if (productViewModel.ProductName?.Length < 2)
            {
                ModelState.AddModelError(nameof(productViewModel.ProductName), "Invalid string length");
            }

            if (!ModelState.IsValid)
            {
                var productsPageViewModel = CreateProductsPageViewModel(productViewModel.CurrentPage, productViewModel);

                return View("Products", productsPageViewModel);
            }

            var category = _categoriesService.GetCategoryByName(productViewModel.Category);
            var supplier = _categoriesService.GetSupplierByName(productViewModel.Supplier);

            var product = _productsService.GetProductById(productViewModel.ProductId);
            if (product != null)
            {
                product.ProductName = productViewModel.ProductName;
                product.QuantityPerUnit = productViewModel.QuantityPerUnit;
                product.UnitPrice = productViewModel.UnitPrice;
                product.CategoryId = category.CategoryId;
                product.SupplierId = supplier.SupplierId;

                _productsService.UpdateProduct(product);
            }
  
            return RedirectToAction("Products",new {page = productViewModel.CurrentPage });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
            return View("ErrorFriendly");
        }
    }
}
