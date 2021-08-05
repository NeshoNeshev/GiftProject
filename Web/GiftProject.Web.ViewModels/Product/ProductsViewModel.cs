namespace GiftProject.Web.ViewModels.Product
{
    using System.Linq;

    using AutoMapper;
    using GiftProject.Services.Mapping;

    public class ProductsViewModel : IMapFrom<Data.Models.Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public int StarRatingsSum { get; set; }

        public string ShortDescription
        {
            get
            {
                var shortDescription = this.Description;
                return shortDescription.Length > 200
                    ? shortDescription.Substring(0, 200) + " ..."
                    : shortDescription;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Product, ProductsViewModel>()
                .ForMember(x => x.StarRatingsSum, options =>
                {
                    options.MapFrom(m => m.ProductVotes.Sum(st => st.Rate));
                });
        }
    }
}
