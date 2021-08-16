namespace GiftProject.Web.ViewModels.Administration.Category
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class InputCategoryModel
    {
        [Required]
        [DisplayName("Име на категорията")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Снимка")]
        public IFormFile ImgUrl { get; set; }
    }
}
