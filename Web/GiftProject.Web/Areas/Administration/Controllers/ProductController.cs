﻿namespace GiftProject.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using GiftProject.Common;
    using GiftProject.Services.Data;
    using GiftProject.Web.CloudinaryHelper;
    using GiftProject.Web.ViewModels.Administration.Category;
    using GiftProject.Web.ViewModels.Administration.Product;
    using GiftProject.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class ProductController : AdministrationController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly Cloudinary cloudinary;
        private readonly IEnumerable<ProductDropDownModel> productDropDown;
        private readonly IEnumerable<CategoryDropDownModel> categoryDropDown;

        public ProductController(IProductService productService, ICategoryService categoryService, Cloudinary cloudinary)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.cloudinary = cloudinary;
            this.categoryDropDown = this.categoryService.GetAll<CategoryDropDownModel>();
            this.productDropDown = this.productService.GetAll<ProductDropDownModel>();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateProduct()
            => this.View(new ProductInputModel { CategoryDropDown = this.categoryDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductInputModel model, IFormFile file)
        {
            model.ImgUrl = await CloudinaryExtension.UploadAsync(this.cloudinary, file);

            if (!this.ModelState.IsValid)
            {
                model.CategoryDropDown = this.categoryDropDown.ToList();
                return this.View(model);
            }

            await this.productService.CreateAsync(model);

            return this.RedirectToAction("AllProduct", "Product", new { area = "Administration" });
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditProduct()
            => this.View(new EditProductModel { ProductDropDown = this.productDropDown.ToList() });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProduct(EditProductModel model, IFormFile file)
        {
            model.NewImgUrl = await CloudinaryExtension.UploadAsync(this.cloudinary, file);

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.productService.EditAsync(model);

            return this.RedirectToAction("AllProduct", "Product", new { area = "Administration" });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove( int id)
        {
            await this.productService.DeleteByIdAsync(id);
            return this.RedirectToAction("AllProduct", "Product", new { area = "Administration" });
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllProduct()
        {
            var model = this.productService.GetAll<ProductsViewModel>();
            var viewModel = new AllProductViewModel { AllProducts = model};
            return this.View(viewModel);
        }
    }
}
