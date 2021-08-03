namespace GiftProject.Web.ViewModels.Product
{
    using GiftProject.Services.Mapping;

    public class ProductsViewModel : IMapFrom<Data.Models.Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public decimal ProductVotesCount { get; set; }
    }
}
