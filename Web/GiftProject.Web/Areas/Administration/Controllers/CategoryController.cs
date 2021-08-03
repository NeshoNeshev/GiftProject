namespace GiftProject.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using GiftProject.Common;
    using GiftProject.Data.Common.Repositories;
    using GiftProject.Data.Models;
    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels.Administration.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateCategory() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory(InputCategoryModel input)
        {
            var exist = this.categoryService.FindByName(input.Name);
            if (exist)
            {
                this.ModelState.AddModelError(nameof(InputCategoryModel.Name), $"Exist {input.Name}");
                return this.View(input);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoryService.CreateAsync(input);
            return this.View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCategory()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCategory(string name)
        {
            return this.View();
        }
    }
}
