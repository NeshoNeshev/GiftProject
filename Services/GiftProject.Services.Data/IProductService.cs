﻿namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Web.ViewModels.Administration.Product;
    using GiftProject.Web.ViewModels.Product;

    public interface IProductService
    {
        Task<int> CreateAsync(ProductInputModel model);

        public IEnumerable<T> GetAll<T>(int? count = null);

        public Task EditAsync(EditProductModel model);

        public Task DeleteByIdAsync(int id);

        public IQueryable<TViewModel> GetAllProductAsQueryable<TViewModel>();

        public Task<ProductsViewModel> GetByNameAsync(string searchString);

        public Task<ProductsViewModel> GetByIdAsync(int id);

        public IEnumerable<T> GetNewProducts<T>(int? count = null);

        public IQueryable<TViewModel> GetAllSearchProductsAsQueryable<TViewModel>();

        public  bool FindByName(string name, int id);
    }
}
