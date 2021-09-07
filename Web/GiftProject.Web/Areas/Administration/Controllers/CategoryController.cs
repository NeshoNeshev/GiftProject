namespace GiftProject.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using GiftProject.Common;
    using GiftProject.Services.Data;
    using GiftProject.Web.CloudinaryHelper;
    using GiftProject.Web.ViewModels.Administration.Category;
    using GiftProject.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        private readonly Cloudinary cloudinary;

        private readonly IEnumerable<CategoryDropDownModel> categoryDropDown;

        public CategoryController(ICategoryService categoryService, Cloudinary cloudinary)
        {
            this.categoryService = categoryService;
            this.cloudinary = cloudinary;
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
            return this.RedirectToAction("CreateProduct", "Product", new { area = "Administration" });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await this.categoryService.DeleteByIdAsync(categoryId);
            return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCategory(int id)
        {
            var category = this.categoryService.GetAll<EditCategoryModel>().FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCategory(int id, EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoryService.EditAsync(model);
            return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllCategory()
        {
            var model = this.categoryService.GetAll<AdminCategoryViewModel>();

            var viewModel = new AdminAllCategoryViewModel() { AllCategories = model };
            return this.View(viewModel);
        }
    }
}
