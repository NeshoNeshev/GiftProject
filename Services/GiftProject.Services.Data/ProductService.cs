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
    using GiftProject.Web.ViewModels.Product;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private const string ConstDate = "2016,1,1";
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly ICatalogueNumber number;
        private readonly ICloudinaryService cloudinaryService;
        private const string AllPaginationFilter = "All";
        private const string DigitPaginationFilter = "0 - 9";

        public ProductService(IDeletableEntityRepository<Product> productRepository, ICatalogueNumber number, ICloudinaryService cloudinaryService)
        {
            this.productRepository = productRepository;
            this.number = number;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(ProductInputModel model)
        {
            //var productExist = await this.productRepository.All().AnyAsync(x => x.Name == model.Name );
            //if (productExist)
            //{
            //    throw new ArgumentException(
            //        string.Format(ExceptionMessages.ProductAlreadyExists, model.Name, model.ImgUrl, model.Description));
            //}

            var coverUrl = await this.cloudinaryService
                .UploadAsync(model.ImgUrl, model.Name);

            var catalogueNumber = String.Empty;

            try
            {
                catalogueNumber = this.number.CreateCatalogueNumber(ConstDate);
            }
            catch (Exception e)
            {
                throw new InvalidCastException("Date not parse");
            }

            var product = new Product
            {
                Name = model.Name,
                ImgUrl = coverUrl,
                CatalogueNumber = catalogueNumber,
                Description = model.Description,
                CategoryId = model.CategoryId,
            };
            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
            return product.Id;
        }

        public async Task EditAsync(EditProductModel model)
        {
            var product = this.productRepository.All().FirstOrDefault(p => p.Id == model.Id);

            if (product == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ProductNotFound, model.Id));
            }

            var imageUrl = string.Empty;
            if (model.NewImgUrl != null)
            {
                imageUrl = await this.cloudinaryService
                   .UploadAsync(model.NewImgUrl, model.Name);
            }
            else
            {
                imageUrl = product.ImgUrl;
            }

            product.Name = model.Name;
            product.ImgUrl = imageUrl;
            product.Description = model.Description;
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
            IQueryable<Product> query = this.productRepository.All().OrderBy(x => x.Name).ThenBy(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetNewProducts<T>(int? count = null)
        {
            IQueryable<Product> query = this.productRepository.All().OrderByDescending(x => x.CreatedOn).Take(3);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IQueryable<TViewModel> GetAllProductAsQueryable<TViewModel>()
        {
            var product = this.productRepository
                .All()
                .OrderByDescending(x => x.ProductVotes.Count)
                .To<TViewModel>();

            return product;
        }

        public IQueryable<TViewModel> GetAllSearchProductsAsQueryable<TViewModel>()
        {
            var product = this.productRepository
                .All()
                .OrderByDescending(x => x.ProductVotes.Count)
                .To<TViewModel>();

            return product;
        }

        public async Task<ProductsViewModel> GetByNameAsync(string searchString)
        {
            var product = await this.productRepository
                .All().To<ProductsViewModel>()
                .FirstOrDefaultAsync(p => p.Name.ToLower() == searchString.ToLower() || p.CatalogueNumber == searchString);

            return product;
        }

        public async Task<ProductsViewModel> GetByIdAsync(int id)
            => await this.productRepository.All().To<ProductsViewModel>().FirstOrDefaultAsync(x => x.Id == id);

        public bool FindByName(string name, int id)
        {
            var exist = this.productRepository.All().Any(x => x.CategoryId == id && x.Name.ToLower().Equals(name.ToLower()));
            return exist;
        }
    }
}
