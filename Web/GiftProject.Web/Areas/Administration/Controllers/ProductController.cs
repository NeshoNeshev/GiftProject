namespace GiftProject.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using GiftProject.Common;
    using GiftProject.Services.Data;
    using GiftProject.Web.CloudinaryHelper;
    using GiftProject.Web.Infrastructure.Pagination;
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
        private const int PageSize = 6;
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
        public async Task<IActionResult> CreateProduct(ProductInputModel model)
        {
            //todo: findByName and FindById

            //todo : "Details", "Product", new { area = string.Empty, model.Id }
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
        public IActionResult EditProduct(int id)
        {
            var product = this.productService.GetAll<EditProductModel>().FirstOrDefault(x => x.Id == id);
            return this.View(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProduct(EditProductModel model)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.productService.EditAsync(model);
            return this.RedirectToAction("ProductDetails", "Product", new { area = "Administration", model.Id });
        }

        [Authorize]
        public async Task<IActionResult> Remove(int productId)
        {
            await this.productService.DeleteByIdAsync(productId);
            return this.RedirectToAction("AllProduct", "Product", new { area = "Administration" });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllProduct(string searchString, string currentFilter, string selectedLetter, int? pageNumber)
        {
            this.ViewData["Current"] = nameof(this.AllProduct);

            this.ViewData["CurrentSearchFilter"] = searchString;
            var product = this.productService
                .GetAllProductAsQueryeable<ProductsViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                product = product.Where(m => m.Name.ToLower().Contains(searchString.ToLower()));
            }

            var productPaginated = await PaginatedList<ProductsViewModel>.CreateAsync(product, pageNumber ?? 1, PageSize);

            var alphabeticalPagingViewModel = new AlphabeticalPagingViewModel
            {
                SelectedLetter = selectedLetter,
            };

            var viewModel = new AllProductViewModel
            {
                ProductsViewModel = productPaginated,
                AlphabeticalProductsViewModel = alphabeticalPagingViewModel,
            };
            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ProductDetails(int id)
        {
            var product = this.productService.GetById<ProductsViewModel>(id);
            if (product == null)
            {
                return this.NotFound();
            }

            var details = new AdministrationProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImgUrl = product.ImgUrl,
                VotesCount = product.VotesCount,
                CatalogueNumber = product.CatalogueNumber,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                CreatedOn = product.CreatedOn,
                ModifiedOn = product.ModifiedOn,
            };

            return this.View(details);
        }
    }
}
