namespace GiftProject.Web.ViewModels.Administration.Category
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class InputCategoryModel
    {
        [Required]
        [DisplayName("Enter Category Name")]
        public string Name { get; set; }
    }
}
