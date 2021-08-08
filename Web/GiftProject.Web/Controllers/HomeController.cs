namespace GiftProject.Web.Controllers
{
    using System.Diagnostics;

    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels;
    using GiftProject.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = this.categoryService.GetAll<CategoryViewModel>();
            var viewModel = new AllCategoryViewModel() { AllCategories = model };
            return this.View(viewModel);
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
