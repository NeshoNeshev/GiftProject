using GiftProject.Web.ViewModels.Category;

namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GiftProject.Web.ViewModels.Administration.Category;

    public interface ICategoryService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);

        public Task CreateAsync(InputCategoryModel model);

        public Task EditAsync(EditCategoryModel model);

        public Task DeleteByIdAsync(int categoryId);

        public bool FindByName(string name);

        Task<T> GetByNameAsync<T>(string name);

        public CategoryViewModel GetById<T>(int id);
    }
}
