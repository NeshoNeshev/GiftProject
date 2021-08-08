namespace GiftProject.Web.ViewModels.Administration.Category
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using GiftProject.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditCategoryModel
    {
        [DisplayName("Категория")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Име на категорията")]
        public string NewName { get; set; }

        [Required]
        [DisplayName("New url")]
        public IFormFile NewImgUrl { get; set; }

        public ICollection<CategoryDropDownModel> CategoryDropDown { get; set; }
    }
}
