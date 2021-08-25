namespace GiftProject.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Services.Data;
    using GiftProject.Web.Infrastructure.Pagination;
    using GiftProject.Web.ViewModels.Category;
    using GiftProject.Web.ViewModels.Details;
    using GiftProject.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IEnumerable<CategoryViewModel> category;
        private const int ProductCount = 6;

        public ProductController(IProductService productService, ICategoryService categoryService, IEnumerable<CategoryViewModel> category)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.category = this.categoryService.GetAll<CategoryViewModel>();
        }

        public async Task<IActionResult> Product(string searchString, string currentFilter, string selectedLetter, int? pageNumber)
        {
            this.ViewData["Current"] = nameof(this.AllProducts);

            this.ViewData["CurrentSearchFilter"] = searchString;
            var product = await Task.Run(() => this.productService
                .GetAllProductAsQueryable<ProductsViewModel>());
            var productsCount = product.Count();
            if (!string.IsNullOrEmpty(searchString))
            {
                var existProduct = this.productService.GetByName(searchString);
                var existNumber = this.productService.GetByCatalogueNumber(searchString);
                if (existProduct != null)
                {
                    return this.RedirectToAction("Details", "Product", new { id = existProduct.Id });
                }

                if (existNumber != null)
                {
                    return this.RedirectToAction("Details", "Product", new { id = existNumber.Id });
                }
              
                    var any = product.Where(m => m.Name.ToLower().Contains(searchString.ToLower()));

                    product = any.Any() ? product.Where(m => m.Name.ToLower().Contains(searchString.ToLower())) : product.Where(x => x.CatalogueNumber.ToLower().Contains(searchString.ToLower()));

            }

            var productPaginated = await PaginatedList<ProductsViewModel>.CreateAsync(product, pageNumber ?? 1, ProductCount);

            var model = this.categoryService.GetAll<CategoryViewModel>();
            var viewModel = new AllCategoryViewModel()
            {
                AllCategories = model,
                Products = productPaginated,

            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> AllProducts(string searchString, string currentFilter, string selectedLetter, int? pageNumber)
        {
            this.ViewData["Current"] = nameof(this.AllProducts);

            this.ViewData["CurrentSearchFilter"] = searchString;
            var product = await Task.Run(() => this.productService
               .GetAllProductAsQueryable<ProductsViewModel>());
            var productsCount = product.Count();
            if (!string.IsNullOrEmpty(searchString))
            {
                var any = product.Where(m => m.Name.ToLower().Contains(searchString.ToLower()));

                product = any.Any() ? product.Where(m => m.Name.ToLower().Contains(searchString.ToLower())) : product.Where(x => x.CatalogueNumber.ToLower().Contains(searchString.ToLower()));
            }

            var productPaginated = await PaginatedList<ProductsViewModel>.CreateAsync(product, pageNumber ?? 1, ProductCount);

            var viewModel = new AllProductViewModel
            {
                ProductsViewModel = productPaginated,
            };
            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var product = this.productService.GetById<ProductsViewModel>(id);
            if (product == null)
            {
                return this.NotFound();
            }

            var details = new DetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImgUrl = product.ImgUrl,
                VotesCount = product.VotesCount,
                CatalogueNumber = product.CatalogueNumber,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                RelatedProducts = this.categoryService.GetById<CategoryViewModel>(product.CategoryId).Products.Where(x => x.Id != product.Id).ToList().Take(3),
            };

            return this.View(details);
        }
    }
}
