using SportsStore.Models;

namespace SportsStore.Data
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product p);

        void CreateProduct(Product p);

        void DeleteProduct(Product p);
    }
}
