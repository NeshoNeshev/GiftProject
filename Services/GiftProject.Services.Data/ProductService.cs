namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Data.Common.Repositories;
    using GiftProject.Data.Models;
    using GiftProject.Services.Mapping;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product>productRepository)
        {
            this.productRepository = productRepository;
        }

        public Task CreateAsync()
        {
            throw new System.NotImplementedException();
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

        public bool FindByNameAsync(string name)
            => this.productRepository
                .All()
                .Any(p => p.Name == name);

        public bool FindByIdAsync(int id)
            => this.productRepository
                .All()
                .Any(p => p.Id == id);

        public Task UpdateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
