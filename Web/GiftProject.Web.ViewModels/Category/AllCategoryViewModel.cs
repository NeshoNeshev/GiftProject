namespace GiftProject.Web.ViewModels.Category
{
    using System.Collections.Generic;

    public class AllCategoryViewModel
    {
        public string Brand { get; set; }

        public IEnumerable<CategoryViewModel> AllCategories { get; set; }
    }
}
