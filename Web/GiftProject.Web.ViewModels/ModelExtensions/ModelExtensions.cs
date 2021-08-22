namespace GiftProject.Web.ViewModels.ModelExtensions
{
    using GiftProject.Web.ViewModels.Details;

    public static class ModelExtensions
    {
        public static string GetInformation(this DetailsViewModel product)
            => product.CategoryName + "-" + product.Name;
    }
}
