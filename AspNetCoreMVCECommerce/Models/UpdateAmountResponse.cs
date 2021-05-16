using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Models.ViewModels;

namespace CodeHome.Models
{
    public class UpdateAmountResponse
    {
        public UpdateAmountResponse(OrderItem orderItem, ShoppingCartViewModel shoppingCartViewModel)
        {
            OrderItem = orderItem;
            ShoppingCartViewModel = shoppingCartViewModel;
        }

        public OrderItem OrderItem { get;  }
        public ShoppingCartViewModel ShoppingCartViewModel { get; }
    }
}
