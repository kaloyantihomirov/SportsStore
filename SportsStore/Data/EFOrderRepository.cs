using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Data
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext context;

        public EFOrderRepository(StoreDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Order> Orders => this.context
            .Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));

            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
        }
    }
}
