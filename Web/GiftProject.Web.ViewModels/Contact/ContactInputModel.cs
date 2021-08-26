namespace GiftProject.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    using GiftProject.Web.Infrastructure.GoogleRecaptchaValidation;

    public class ContactInputModel
    {
        [Required]
        [Display(Name = "Вашите имена")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Вашият email адрес")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Заглавие на съобщението")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Съдържание на съобщението")]
        public string Message { get; set; }

        //[GoogleRecaptchaValidation]
        //public string RecaptchaValue { get; set; }
    }
}
