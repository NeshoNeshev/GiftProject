using System.Collections.Generic;
using GiftProject.Services.Mapping;
using GiftProject.Web.ViewModels.Product;

namespace GiftProject.Web.ViewModels.Category
{
    public class CategoryDropDownModel : IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductsViewModel> Products { get; set; }
    }
}
