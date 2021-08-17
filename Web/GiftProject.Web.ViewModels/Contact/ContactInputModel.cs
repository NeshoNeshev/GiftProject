namespace GiftProject.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    using GiftProject.Web.Infrastructure.GoogleRecaptchaValidation;

    public class ContactInputModel
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Your Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [GoogleRecaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
