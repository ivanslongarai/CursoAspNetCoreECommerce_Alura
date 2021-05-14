using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodeHome.Controllers
{
    public class OrderController : Controller
    {

        public IActionResult Carousel()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Resume()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

    }
}
