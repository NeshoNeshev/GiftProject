namespace GiftProject.Web.ViewModels.Index
{
    using System.Collections.Generic;

    using GiftProject.Web.ViewModels.Category;
    using GiftProject.Web.ViewModels.Product;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoryViewModel> CategoryViewModels { get; set; }

        public IEnumerable<ProductsViewModel> ProductsViewModels { get; set; }
    }
}
