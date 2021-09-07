namespace GiftProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Services.Data;
    using GiftProject.Web.Infrastructure.Pagination;
    using GiftProject.Web.ViewModels.Category;
    using GiftProject.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private const int ProductCount = 6;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public async Task<IActionResult> CategoryById(int id, string searchString, string currentFilter, string selectedLetter, int? pageNumber)
        {

            this.ViewData["CurrentSearchFilter"] = searchString;

            var product = await Task.Run(() => this.productService
                .GetAllProductAsQueryable<ProductsViewModel>().Where(x => x.CategoryId == id));

            //var searchProduct = await Task.Run(() => this.productService
            //    .GetAllSearchProductsAsQueryable<ProductsViewModel>());

            if (!string.IsNullOrEmpty(searchString))
            {
                var existProduct = this.productService.GetByNameAsync(searchString);
                if (existProduct.Result != null)
                {
                    return this.RedirectToAction("Details", "Product", new { id = existProduct.Result.Id });
                }

                var any = product.Where(m => m.Name.ToLower().Contains(searchString.ToLower()));
                product = any.Any() ? product.Where(m => m.Name.ToLower().Contains(searchString.ToLower())) : product.Where(x => x.CatalogueNumber.ToLower().Contains(searchString.ToLower()));
            }

            var productPaginated = await PaginatedList<ProductsViewModel>.CreateAsync(product, pageNumber ?? 1, ProductCount);

            var all = this.categoryService.GetAll<CategoryViewModel>();

            var viewModel = new AllCategoryViewModel
            {
                AllCategories = all,
                Products = productPaginated,
            };

            return this.View(viewModel);
        }

        public IActionResult AllCategories()
        {
            var model = this.categoryService.GetAll<CategoryViewModel>();
            var viewModel = new AllCategoryViewModel() { AllCategories = model };
            return this.View(viewModel);
        }
    }
}
