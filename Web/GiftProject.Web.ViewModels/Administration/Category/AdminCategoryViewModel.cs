namespace GiftProject.Web.ViewModels.Administration.Category
{
    using GiftProject.Services.Mapping;

    public class AdminCategoryViewModel : IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public int ProductsCount { get; set; }
    }
}
