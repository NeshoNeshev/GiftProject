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
    using GiftProject.Web.ViewModels.Administration.Product;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(ProductInputModel model)
        {
            var productExist = await this.productRepository.All().AnyAsync(x => x.Id == model.Id);
            if (productExist)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.ProductAlreadyExists, model.Name, model.ImgUrl, model.Description));
            }

            //Todo : ProductVote

            var product = new Product
            {
                Name = model.Name,
                ImgUrl = model.ImgUrl,
                Description = model.Description,
                CategoryId = model.CategoryId,
            };
            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public async Task EditAsync(EditProductModel model)
        {
            var product = this.productRepository.All().FirstOrDefault(p => p.Id == model.Id);

            if (product == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ProductNotFound, model.Id));
            }

            product.Name = model.NewName;
            product.ImgUrl = model.NewImgUrl;
            product.Description = model.NewDescription;
            product.ModifiedOn = DateTime.UtcNow;

            this.productRepository.Update(product);
            await this.productRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = this.productRepository.All().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ProductNotFound, id));
            }

            product.IsDeleted = true;
            product.DeletedOn = DateTime.UtcNow;
            this.productRepository.Update(product);
            await this.productRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Product> query = this.productRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
