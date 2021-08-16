namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Web.ViewModels.Administration.Product;
    using GiftProject.Web.ViewModels.Product;

    public interface IProductService
    {
        Task CreateAsync(ProductInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public Task EditAsync(EditProductModel model);

        public Task DeleteByIdAsync(int id);

        public IQueryable<TViewModel> GetAllProductAsQueryeable<TViewModel>();

        public IQueryable<TViewModel> GetAllProductsByFilterAsQueryeable<TViewModel>(string letter = null);

        public ProductsViewModel GetById<T>(int id);

        public IEnumerable<ProductsViewModel> NewProducts<T>();
    }
}
