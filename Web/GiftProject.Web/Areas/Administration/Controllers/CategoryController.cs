namespace GiftProject.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Common;
    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels.Administration.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        private readonly IEnumerable<CategoryDropDownModel> categoryDropDown;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.categoryDropDown = this.categoryService.GetAll<CategoryDropDownModel>();
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
            return this.RedirectToAction("CreateProduct", "Product", new {area = "Administration"});
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCategory()
            => this.View(new EditCategoryModel() { CategoryDropDown = this.categoryDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCategory(EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoryService.EditAsync(model);
            return this.View();
        }
    }
}
