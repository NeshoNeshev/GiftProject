namespace GiftProject.Web.ViewModels.Administration.Product
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditProductModel
    {
        [DisplayName("Product")]
        public int Id { get; set; }

        [Required]
        [DisplayName("New Name")]
        public string NewName { get; set; }

        [Required]
        [DisplayName("New Img Url")]
        public string NewImgUrl { get; set; }

        [Required]
        [DisplayName("New Description")]
        public string NewDescription { get; set; }

        public ICollection<ProductDropDownModel> ProductDropDown { get; set; }
    }
}
