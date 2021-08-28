namespace GiftProject.Web.ViewModels.Product
{
    using GiftProject.Web.Infrastructure.Pagination;

    public class AllProductViewModel
    {
        public PaginatedList<ProductsViewModel> ProductsViewModel { get; set; }
    }
}
