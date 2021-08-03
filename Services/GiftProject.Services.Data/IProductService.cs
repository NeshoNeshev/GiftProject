namespace GiftProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task CreateAsync();

        public IEnumerable<T> GetAll<T>(int? count = null);

        public bool FindByNameAsync(string name);

        public bool FindByIdAsync(int id);

        public Task UpdateAsync();

    }
}
