namespace GiftProject.Web.Controllers
{
    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult CategoryById(int id)
        {
            var category = this.categoryService.GetById<CategoryViewModel>(id);

            return this.View(category);
        }
    }
}
