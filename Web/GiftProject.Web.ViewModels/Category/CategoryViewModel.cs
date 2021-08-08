namespace GiftProject.Web.ViewModels.Category
{
    using System.Collections.Generic;

    using GiftProject.Services.Mapping;
    using GiftProject.Web.ViewModels.Product;

    public class CategoryViewModel : IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public IEnumerable<ProductsViewModel> Products { get; set; }
    }
}
