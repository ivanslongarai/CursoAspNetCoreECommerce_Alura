using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CodeHome.Models.ViewModels;

namespace CodeHome.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder();
        void AddItem(string internalId);
        UpdateAmountResponse UpdateAmount(OrderItem orderItem);

    }

    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IHttpContextAccessor ContextAccessor;
        private readonly IOrderItemRepository OrderItemRepository;

        public OrderRepository(ApplicationContext context, IHttpContextAccessor contextAccessor,
            IOrderItemRepository orderItemRepository) : base(context)
        {
            this.ContextAccessor = contextAccessor;
            this.OrderItemRepository = orderItemRepository;
        }

        public void AddItem(string internalId)
        {
            var product = Context.Set<Product>().Where(p => p.InternalId == internalId).SingleOrDefault();
            if (product == null)
                throw new ArgumentException($"Product InternalId: {internalId} is missing");
            var order = GetOrder();
            var orderItem = Context.Set<OrderItem>().Where(i => i.Product.InternalId == internalId 
                && i.Order.Id == order.Id).SingleOrDefault();

            if (orderItem == null)
            {
                orderItem = new OrderItem(order, product, 1, product.Price);
                Context.Set<OrderItem>().Add(orderItem);
                Context.SaveChanges();
            }

        }

        public Order GetOrder()
        {
            var orderId = GetOrderId();
            var order = dbSet
                .Include(p => p.Items)
                .ThenInclude(i => i.Product)
                .Where(p => p.Id == orderId).SingleOrDefault();

            if (order == null)
            {
                order = new Order();
                dbSet.Add(order);
                Context.SaveChanges();
                SetOrderId(order.Id);
            }

            return order;
        }

        private int? GetOrderId()
        {
            return ContextAccessor.HttpContext.Session.GetInt32("orderId");
        }

        private void SetOrderId(int orderId)
        {
            ContextAccessor.HttpContext.Session.SetInt32("orderId", orderId);
        }

        public UpdateAmountResponse UpdateAmount(OrderItem orderItem)
        {
            var orderItemBD = OrderItemRepository.GetOrderItem(orderItem.Id);
            
            if (orderItemBD != null)
            {
                if (orderItem.Amount <= 0)
                    OrderItemRepository.RemoveOrdemItem(orderItem.Id);
 
                orderItemBD.SetAmount(orderItem.Amount);
                Context.SaveChanges();


                var shoppingCartViewModel = new ShoppingCartViewModel(GetOrder().Items);
                return new UpdateAmountResponse(orderItemBD, shoppingCartViewModel);
            }

            throw new ArgumentException("Item Order is missing...");
        }

    }
}
