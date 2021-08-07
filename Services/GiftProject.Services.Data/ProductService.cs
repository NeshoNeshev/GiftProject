using GiftProject.Web.ViewModels.Product;

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
        private const string AllPaginationFilter = "All";
        private const string DigitPaginationFilter = "0 - 9";

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(ProductInputModel model)
        {
            var productExist = await this.productRepository.All().AnyAsync(x => x.Name == model.Name);
            if (productExist)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.ProductAlreadyExists, model.Name, model.ImgUrl, model.Description));
            }

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

        public IQueryable<TViewModel> GetAllProductAsQueryeable<TViewModel>()
        {
            var product = this.productRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TViewModel>();

            return product;
        }

        public IQueryable<TViewModel> GetAllProductsByFilterAsQueryeable<TViewModel>(string letter = null)
        {
            var productByFilter = Enumerable.Empty<TViewModel>().AsQueryable();

            if (!string.IsNullOrEmpty(letter) && letter != AllPaginationFilter && letter != DigitPaginationFilter)
            {
                productByFilter = this.productRepository
                    .All()
                    .Where(x => x.Name.ToLower().StartsWith(letter))
                    .To<TViewModel>();
            }
            else if (letter == DigitPaginationFilter)
            {
                var numbers = Enumerable.Range(0, 10).Select(i => i.ToString());
                productByFilter = this.productRepository
                    .All()
                    .Where(x => numbers.Contains(x.Name.Substring(0, 1)))
                    .To<TViewModel>();
            }
            else
            {
                productByFilter = this.GetAllProductAsQueryeable<TViewModel>();
            }

            return productByFilter;
        }

        public ProductsViewModel GetById<T>(int id)
            => this.productRepository.All().To<ProductsViewModel>().FirstOrDefault(x => x.Id == id);
    }
}
