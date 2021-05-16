using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeHome.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel(IList<OrderItem> items)
        {
            Items = items;
        }

        public IList<OrderItem> Items { get;  }
        public decimal Total => Items.Sum(i => i.Amount * i.UnitPrice);




    }
}
