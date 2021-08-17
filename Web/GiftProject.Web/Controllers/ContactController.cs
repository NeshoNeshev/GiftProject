namespace GiftProject.Web.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using GiftProject.Common;
    using GiftProject.Services.Messaging;
    using GiftProject.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.emailSender.SendEmailAsync(
                GlobalConstants.SenderEmail,
                model.Name,
                GlobalConstants.Email,
                model.Subject,
                this.ModifyContentMessage(model.Message, model.Email));

            return this.RedirectToAction("ThankYou");
        }

        private string ModifyContentMessage(string message, string email)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            sb.AppendLine(email);
            return sb.ToString();
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
