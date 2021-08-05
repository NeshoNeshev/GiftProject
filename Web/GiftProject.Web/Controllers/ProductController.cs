namespace GiftProject.Web.Controllers
{
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

        private const int PageSize = 6;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Product(string searchString, string currentFilter, string selectedLetter, int? pageNumber)
        {
            this.ViewData["Current"] = nameof(this.Product);
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewData["CurrentSearchFilter"] = searchString;
            var movies = this.productService
                .GetAllMoviesByFilterAsQueryeable<ProductsViewModel>(selectedLetter);

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Name.ToLower().Contains(searchString.ToLower()));
            }

            var moviesPaginated = await PaginatedList<ProductsViewModel>.CreateAsync(movies, pageNumber ?? 1, PageSize);

            var alphabeticalPagingViewModel = new AlphabeticalPagingViewModel
            {
                SelectedLetter = selectedLetter,
            };

            var viewModel = new AllProductViewModel
            {
                ProductsViewModel = moviesPaginated,
                AlphabeticalProductsViewModel = alphabeticalPagingViewModel,
            };

            return this.View(viewModel);
        }
    }
}
