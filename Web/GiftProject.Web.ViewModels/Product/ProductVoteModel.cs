namespace GiftProject.Web.ViewModels.Product
{
    using System;

    public class ProductVoteModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        //public string ApplicationUserId { get; set; }

        public int Rate { get; set; }

        public DateTime NextVoteDate { get; set; }
    }
}
