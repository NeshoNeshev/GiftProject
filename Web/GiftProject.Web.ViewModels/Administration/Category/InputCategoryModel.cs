using Microsoft.AspNetCore.Http;

namespace GiftProject.Web.ViewModels.Administration.Category
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class InputCategoryModel
    {
        [Required]
        [DisplayName("Име на категорията")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Snimka")]
        public IFormFile ImgUrl { get; set; }
    }
}
