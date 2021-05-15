using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CodeHome.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public int Id { get; set; }
    }

    public class Product : BaseModel
    {
        public Product()
        {

        }

        [Required]
        public string InternalId { get; protected set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public decimal Price { get; private set; }

        public Product(string internalId, string name, decimal price)
        {
            this.InternalId = internalId;
            this.Name = name;
            this.Price = price;
        }
    }

    public class Register : BaseModel
    {
        public Register()
        {
        }

        public virtual Order Order { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Phone { get; set; } = "";
        [Required]
        public string Adress { get; set; } = "";
        [Required]
        public string Complement { get; set; } = "";
        [Required]
        public string District { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        [Required]
        public string State { get; set; } = "";
        [Required]
        public string ZipCode { get; set; } = "";
    }

    public class OrderItem : BaseModel
    {   
        [Required]
        public Order Order { get; private set; }
        [Required]
        public Product Product { get; private set; }
        [Required]
        public int Amount { get; private set; }
        [Required]
        public decimal UnitPrice { get; private set; }

        public OrderItem()
        {

        }

        public OrderItem(Order order, Product product, int amount, decimal unitprice)
        {
            Order = order;
            Product = product;
            Amount = amount;
            UnitPrice = unitprice;
        }
    }

    public class Order : BaseModel
    {
        public Order()
        {
            Register = new Register();
        }

        public Order(Register register)
        {
            Register = register;
        }

        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        [Required]
        public virtual Register Register { get; private set; }
    }
}
