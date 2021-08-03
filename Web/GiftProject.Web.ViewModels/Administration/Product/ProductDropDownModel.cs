namespace GiftProject.Web.ViewModels.Administration.Product
{
    using GiftProject.Services.Mapping;

    public class ProductDropDownModel : IMapFrom<Data.Models.Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
