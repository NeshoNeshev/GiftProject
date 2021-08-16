namespace GiftProject.Web.ViewModels.Administration.Product
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class EditProductModel
    {
        [DisplayName("Продукт")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Ново Име")]
        public string NewName { get; set; }

        [Required]
        [DisplayName("Нова снимка")]
        public IFormFile NewImgUrl { get; set; }

        [Required]
        [DisplayName("Ново описание")]
        public string NewDescription { get; set; }

        public ICollection<ProductDropDownModel> ProductDropDown { get; set; }
    }
}
