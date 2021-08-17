using System.Linq;
using GiftProject.Web.ViewModels.Product;

namespace GiftProject.Web.Controllers
{
    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public IActionResult CategoryById(int id)
        {
            var category = this.categoryService.GetById<CategoryViewModel>(id);

            return this.View(category);
        }

        public IActionResult AllCategories()
        {
            var model = this.categoryService.GetAll<CategoryViewModel>();
            var viewModel = new AllCategoryViewModel() { AllCategories = model };
            return this.View(viewModel);
        }

        public JsonResult GetCategories()
        {
            var categories = this.categoryService.GetAll<CategoryViewModel>().ToList();
            return this.Json(categories);
        }

        public JsonResult GetProducts(int? id)
        {
            var types = this.productService.GetAll<ProductsViewModel>().Where(x => x.CategoryId == id).ToList();
            return this.Json(types);
        }
    }
}
