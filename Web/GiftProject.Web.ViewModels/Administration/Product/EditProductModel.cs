namespace GiftProject.Web.ViewModels.Administration.Product
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using GiftProject.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditProductModel : IMapFrom<Data.Models.Product>
    {
        [DisplayName("ID : ")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Ново Име")]
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        [DisplayName("Нова снимка")]
        public IFormFile NewImgUrl { get; set; }

        [Required]
        [DisplayName("Ново описание")]
        public string Description { get; set; }
    }
}
