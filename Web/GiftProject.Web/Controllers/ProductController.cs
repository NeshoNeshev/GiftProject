namespace GiftProject.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Services.Data;
    using GiftProject.Web.Infrastructure.Pagination;
    using GiftProject.Web.ViewModels.Category;
    using GiftProject.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IEnumerable<CategoryViewModel> category;
        private const int PageSize = 6;

        public ProductController(IProductService productService, ICategoryService categoryService, IEnumerable<CategoryViewModel> category)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.category = this.categoryService.GetAll<CategoryViewModel>();
        }

        public async Task<IActionResult> Product()
        {
            var model = this.categoryService.GetAll<CategoryViewModel>();
            var viewModel = new AllCategoryViewModel() {AllCategories = model};
            return this.View(viewModel);
        }

        public async Task<IActionResult> AllProducts(string searchString, string currentFilter, string selectedLetter, int? pageNumber)
        {
            this.ViewData["Current"] = nameof(this.AllProducts);
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewData["CurrentSearchFilter"] = searchString;
            var product = this.productService
                .GetAllProductsByFilterAsQueryeable<ProductsViewModel>(selectedLetter);

            if (!string.IsNullOrEmpty(searchString))
            {
                var any = product.Where(m => m.Name.ToLower().Contains(searchString.ToLower()));

                product = any.Any() ? product.Where(m => m.Name.ToLower().Contains(searchString.ToLower())) : product.Where(x => x.CatalogueNumber.ToLower().Contains(searchString.ToLower()));
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

        public IActionResult Details(int id)
        {
            var product = this.productService.GetById<ProductsViewModel>(id);
            return this.View(product);
        }
    }
}
