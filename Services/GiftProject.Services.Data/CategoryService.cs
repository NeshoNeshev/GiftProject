using Microsoft.EntityFrameworkCore;

namespace GiftProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Data.Common.Repositories;
    using GiftProject.Data.Models;
    using GiftProject.Services.Data.Common;
    using GiftProject.Services.Mapping;
    using GiftProject.Web.ViewModels.Administration.Category;

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

        public async Task EditAsync(EditCategoryModel model)
        {
            var category = this.categoryRepository.All().FirstOrDefault(p => p.Id == model.Id);

            if (category == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.CategoryNotFound, model.Id));
            }

            category.Name = model.NewName;
            category.ModifiedOn = DateTime.UtcNow;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = this.categoryRepository.All().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ProductNotFound, id));
            }

            product.IsDeleted = true;
            product.DeletedOn = DateTime.UtcNow;
            this.categoryRepository.Update(product);
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

        public bool FindByName(string name)
            => this.categoryRepository
                .All()
                .Any(c => c.Name == name);

        public async Task<T> GetByNameAsync<T>(string name)
            => await this.categoryRepository.All().Where(x => x.Name == name).To<T>().FirstOrDefaultAsync();


        public CategoryDropDownModel GetByIdAsync<T>(int id)
        {
            var result = this.categoryRepository.All().To<CategoryDropDownModel>().FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
