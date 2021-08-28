namespace GiftProject.Web.ViewModels.Administration.Category
{
    using System.Collections.Generic;

    using GiftProject.Services.Mapping;

    public class AdminAllCategoryViewModel : IMapFrom<Data.Models.Category>
    {
        public IEnumerable<AdminCategoryViewModel> AllCategories { get; set; }
    }
}
