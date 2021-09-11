﻿namespace GiftProject.Services.Data
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
    using GiftProject.Web.ViewModels.Category;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Product> productRepository;

        public CategoryService(): this(null,null, null)
        {
            
        }
        public CategoryService(IDeletableEntityRepository<Category> categoryRepository, ICloudinaryService cloudinaryService, IDeletableEntityRepository<Product> productRepository)
        {
            this.categoryRepository = categoryRepository;
            this.cloudinaryService = cloudinaryService;
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(InputCategoryModel model)
        {
            var existCategory = this.categoryRepository.All().Any(x => x.Name == model.Name);
            if (existCategory)
            {
                return;
            }

            var coverUrl = await this.cloudinaryService
                .UploadAsync(model.ImgUrl, model.Name);
            var category = new Category
            {
                Name = model.Name,
                ImgUrl = coverUrl,
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

            var url = String.Empty;

            if (model.NewImgUrl != null)
            {
                url = await this.cloudinaryService
                               .UploadAsync(model.NewImgUrl, model.Name);
            }
            else
            {
                url = category.ImgUrl;
            }

            category.Name = model.Name;
            category.ImgUrl = url;
            category.ModifiedOn = DateTime.UtcNow;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int categoryId)
        {
            var category = this.categoryRepository.All().FirstOrDefault(x => x.Id == categoryId);
            if (category == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ProductNotFound, categoryId));
            }

            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();

            var products = await this.productRepository.All().Where(x => x.CategoryId == categoryId).ToListAsync();
            foreach (var product in products)
            {
                product.IsDeleted = true;
                product.DeletedOn = DateTime.UtcNow;
                this.productRepository.Update(product);
            }

            await this.productRepository.SaveChangesAsync();
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

        public bool FindById(int id)
            => this.categoryRepository
                .All()
                .Any(c => c.Id == id);

        public async Task<T> GetByNameAsync<T>(string name)
            => await this.categoryRepository.All().Where(x => x.Name == name).To<T>().FirstOrDefaultAsync();

        public CategoryViewModel GetById<T>(int id)
        {
            var result = this.categoryRepository.All().To<CategoryViewModel>().FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
