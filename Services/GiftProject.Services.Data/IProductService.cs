namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GiftProject.Web.ViewModels.Administration.Product;

    public interface IProductService
    {
        Task CreateAsync(ProductInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public Task EditAsync(EditProductModel model);

        public Task DeleteByIdAsync(int id);
    }
}
