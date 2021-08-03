namespace GiftProject.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Common;
    using GiftProject.Data.Common.Repositories;
    using GiftProject.Data.Models;
    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels.Administration.Category;
    using GiftProject.Web.ViewModels.Administration.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class ProductController : AdministrationController
    {
        private readonly IProductService productService;
        private readonly ICategoryService _categoryService;

        private readonly IEnumerable<CategoryDropDownModel> categoryDropDown;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            _categoryService = categoryService;
            this.categoryDropDown = this._categoryService.GetAll<CategoryDropDownModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateProduct()
            => this.View(new ProductInputModel { CategoryDropDown = this.categoryDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductInputModel model)
        {
            var exist = this.productService.FindByNameAsync(model.Name);
            if (exist)
            {
                this.ModelState.AddModelError(nameof(InputCategoryModel.Name), $"Exist {model.Name}");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            return this.View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditProduct()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProduct(string name)
        {
            return this.View();
        }
    }
}
