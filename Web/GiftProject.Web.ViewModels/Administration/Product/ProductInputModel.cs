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

        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [DisplayName("Img Url")]
        public string ImgUrl { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public ICollection<CategoryDropDownModel> CategoryDropDown { get; set; }
    }
}
