namespace GiftProject.Web.ViewModels.Administration.Category
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using GiftProject.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditCategoryModel : IMapFrom<Data.Models.Category>
    {
        [DisplayName("Категория")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Ново име")]
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        [DisplayName("Снимка")]
        public IFormFile NewImgUrl { get; set; }

        //public ICollection<CategoryDropDownModel> CategoryDropDown { get; set; }
    }
}
