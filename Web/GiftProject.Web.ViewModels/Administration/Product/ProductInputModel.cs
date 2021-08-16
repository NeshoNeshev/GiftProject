namespace GiftProject.Web.ViewModels.Administration.Product
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using GiftProject.Services.Mapping;
    using GiftProject.Web.ViewModels.Administration.Category;
    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Име на продукта")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Снимка")]
        public IFormFile ImgUrl { get; set; }

        [Required]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Категория")]
        public int CategoryId { get; set; }

        public ICollection<CategoryDropDownModel> CategoryDropDown { get; set; }
    }
}
