using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;

namespace CodeHome.Repositories
{
    public interface IOrderItemRepository
    {
        OrderItem GetOrderItem(int orderItemId);
        void RemoveOrdemItem(int orderItemId);
    }

    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext context) : base(context)
        {
        }

        public OrderItem GetOrderItem(int orderItemId)
        {
            return dbSet
                .Where(oi => oi.Id == orderItemId)
                .SingleOrDefault();
        }

        public void RemoveOrdemItem(int orderItemId)
        {
            dbSet.Remove(GetOrderItem(orderItemId));
        }
    }
}
