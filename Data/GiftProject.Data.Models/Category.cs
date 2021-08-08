namespace GiftProject.Data.Models
{
    using System.Collections.Generic;

    using GiftProject.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
