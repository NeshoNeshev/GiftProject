using System;

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

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CatalogueNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public int VotesCount { get; set; }

        public string ShortDescription
        {
            get
            {
                var shortDescription = this.Description;
                return shortDescription.Length > 50
                    ? shortDescription.Substring(0, 50) + " ..."
                    : shortDescription;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Product, ProductsViewModel>()
                .ForMember(x => x.VotesCount, options =>
                {
                    options.MapFrom(m => m.ProductVotes.Sum(pv => (int)pv.Vote));
                });
        }
    }
}
