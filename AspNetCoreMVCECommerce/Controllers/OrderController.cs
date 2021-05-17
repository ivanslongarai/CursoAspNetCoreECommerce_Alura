using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;
using CodeHome.Models.ViewModels;
using CodeHome.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeHome.Controllers
{
    public class OrderController : Controller
    {

        private readonly IProductRepository ProductRepository;
        private readonly IOrderRepository OrderRepository;
        private readonly IOrderItemRepository OrderItemRepository;

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository)
        {
            this.ProductRepository = productRepository;
            this.OrderRepository = orderRepository;
            this.OrderItemRepository = orderItemRepository;
        }

        public IActionResult Carousel()
        {            
            return View(ProductRepository.GetProducts());
        }

        public IActionResult Register()
        {
            var orderRepository = OrderRepository.GetOrder();

            if (orderRepository == null)
                return RedirectToAction("Carousel");

            return View(orderRepository.Register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Avoiding CSRF (Croos-site request forgery) problem
        public IActionResult Resume(Register register)
        {
            if (ModelState.IsValid)
            {
                Order order = OrderRepository.UpdateRegister(register);
                return View(order);
            }

            return RedirectToAction("Register");
        }

        public IActionResult ShoppingCart(string internalId)
        {
            if (!string.IsNullOrEmpty(internalId))
                OrderRepository.AddItem(internalId);

            Order order = OrderRepository.GetOrder();
            List<OrderItem> items = order.Items;
            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel(items);
            return base.View(shoppingCartViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken] // Avoiding CSRF (Croos-site request forgery) problem
        public UpdateAmountResponse UpdateAmount([FromBody]OrderItem orderItem)
        {
            return OrderRepository.UpdateAmount(orderItem);
        }

    }
}
