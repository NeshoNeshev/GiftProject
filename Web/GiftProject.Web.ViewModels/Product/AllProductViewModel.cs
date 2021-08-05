namespace GiftProject.Web.ViewModels.Product
{
    using System.Collections.Generic;

    using GiftProject.Web.Infrastructure.Pagination;

    public class AllProductViewModel
    {
        public IEnumerable<ProductsViewModel> AllProducts { get; set; }

        public PaginatedList<ProductsViewModel> ProductsViewModel { get; set; }

        public AlphabeticalPagingViewModel AlphabeticalProductsViewModel { get; set; }
    }
}
