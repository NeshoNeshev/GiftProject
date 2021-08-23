namespace GiftProject.Web.ViewModels.Administration.Product
{
    using System;

    using GiftProject.Services.Mapping;

    public class AdministrationProductDetailsViewModel : IMapFrom<Data.Models.Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CatalogueNumber { get; set; }

        public string Description { get; set; }

        public int VotesCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

    }
}
