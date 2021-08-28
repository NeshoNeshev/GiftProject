namespace GiftProject.Web.ViewModels.Category
{
    using System.Collections.Generic;

    using GiftProject.Web.Infrastructure.Pagination;
    using GiftProject.Web.ViewModels.Product;

    public class AllCategoryViewModel
    {
        public IEnumerable<CategoryViewModel> AllCategories { get; set; }

        public PaginatedList<ProductsViewModel> Products { get; set; }

    }
}
