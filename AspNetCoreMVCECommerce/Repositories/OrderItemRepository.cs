using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;

namespace CodeHome.Repositories
{
    public interface IOrderItemRepository
    {

    }

    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
