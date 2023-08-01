using SportsStore.Models;

namespace SportsStore.Data
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Product> Products => throw new NotImplementedException();
    }
}
