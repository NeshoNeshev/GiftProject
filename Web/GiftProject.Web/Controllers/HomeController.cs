namespace GiftProject.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels;
    using GiftProject.Web.ViewModels.Category;
    using GiftProject.Web.ViewModels.Index;
    using GiftProject.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public HomeController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var categoryModel = this.categoryService.GetAll<IndexCategoryViewModel>();

            var productModel = this.productService.GetNewProducts<ProductsViewModel>();
            var viewModel = new IndexViewModel
            {
                CategoryViewModels = categoryModel,
                ProductsViewModels = productModel,

            };
            return this.View(viewModel);
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult HttpError(int statusCode)
        {
            if (statusCode == 404)
            {
                return this.View("404", statusCode);
            }

            return this.View("Error");
        }


        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
