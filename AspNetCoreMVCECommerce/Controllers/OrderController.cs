using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models;
using CodeHome.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeHome.Controllers
{
    public class OrderController : Controller
    {

        private readonly IProductRepository ProductRepository;
        private readonly IOrderRepository OrderRepository;

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.ProductRepository = productRepository;
            this.OrderRepository = orderRepository;
        }

        public IActionResult Carousel()
        {            
            return View(ProductRepository.GetProducts());
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Resume()
        {
            Order order = OrderRepository.GetOrder();
            return View(order);
        }

        public IActionResult ShoppingCart(string internalId)
        {
            if (!string.IsNullOrEmpty(internalId))
                OrderRepository.AddItem(internalId);

            Order order = OrderRepository.GetOrder();
            return View(order.Items);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
