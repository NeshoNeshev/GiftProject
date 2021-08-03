namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GiftProject.Web.ViewModels.Administration.Category;

    public interface ICategoryService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);

        public Task CreateAsync(InputCategoryModel model);

        public bool FindByName(string name);
    }
}
