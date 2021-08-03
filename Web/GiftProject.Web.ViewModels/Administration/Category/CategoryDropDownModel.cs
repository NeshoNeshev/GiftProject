namespace GiftProject.Web.ViewModels.Administration.Category
{
    using GiftProject.Services.Mapping;

    public class CategoryDropDownModel :IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
