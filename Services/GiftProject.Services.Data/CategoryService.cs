using System;
using System.Threading.Tasks;
using GiftProject.Web.ViewModels.Administration.Category;

namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using GiftProject.Data.Common.Repositories;
    using GiftProject.Data.Models;
    using GiftProject.Services.Mapping;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(InputCategoryModel model)
        {
            var existCategory = this.categoryRepository.All().Any(x => x.Name == model.Name);
            if (existCategory)
            {
                return;
            }

            var category = new Category
            {
                Name = model.Name,
            };
            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoryRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool FindByName(string name) => this.categoryRepository
                .All()
                .Any(c => c.Name == name);

    }
}
