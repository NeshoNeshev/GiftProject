namespace GiftProject.Web.Controllers
{
    using System.Linq;

    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public IActionResult Product()
        {
            var viewModel = new AllCategoryViewModel();
            var model = this.categoryService.GetAll<CategoryViewModel>().ToList();
            viewModel.AllCategories = model;
            return this.View(viewModel);
        }
    }
}
