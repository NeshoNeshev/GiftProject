namespace GiftProject.Web.ViewModels.Index
{
    using GiftProject.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }
    }
}
