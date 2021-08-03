namespace GiftProject.Data.Models
{
    using System.Collections.Generic;

    using GiftProject.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.ProductVotes = new HashSet<ProductVote>();
        }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<ProductVote> ProductVotes { get; set; }
    }
}
