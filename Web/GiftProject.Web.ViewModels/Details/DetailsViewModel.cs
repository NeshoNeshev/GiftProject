namespace GiftProject.Web.ViewModels.Details
{
    using System.Collections.Generic;

    using GiftProject.Services.Mapping;
    using GiftProject.Web.ViewModels.Product;

    public class DetailsViewModel : IMapFrom<Data.Models.Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CatalogueNumber { get; set; }

        public string Description { get; set; }

        public int VotesCount { get; set; }

        public IEnumerable<ProductsViewModel> RelatedProducts { get; set; }
    }
}
