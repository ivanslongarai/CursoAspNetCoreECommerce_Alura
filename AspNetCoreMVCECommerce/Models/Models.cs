using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CodeHome.Models
{

    static class Constants
    {
        public const string MsgError = "This field value is needed.";
    }

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
        
        [MinLength(5, ErrorMessage ="This field requires a minimum of 5 caracteres")]
        [MaxLength(50, ErrorMessage ="This field requires a maximum of 50 caracteres")]
        [Required(ErrorMessage = Constants.MsgError)]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string Phone { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string Adress { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string Complement { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string District { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string City { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string State { get; set; } = "";
        [Required(ErrorMessage = Constants.MsgError)]
        public string ZipCode { get; set; } = "";

        internal void Update(Register newRegister)
        {
            this.Name = newRegister.Name;
            this.Email = newRegister.Email;
            this.Phone = newRegister.Phone;
            this.Adress = newRegister.Adress;
            this.Complement = newRegister.Complement;
            this.District = newRegister.District;
            this.City = newRegister.City;
            this.State = newRegister.State;
            this.ZipCode = newRegister.ZipCode;
        }
    }

    [DataContract]
    public class OrderItem : BaseModel
    {   
        [Required]
        [DataMember]
        public Order Order { get; private set; }
        [Required]
        [DataMember]
        public Product Product { get; private set; }
        [Required]
        [DataMember]
        public int Amount { get; private set; }
        [Required]
        [DataMember]
        public decimal UnitPrice { get; private set; }
        [DataMember]
        public decimal Subtotal => Amount * UnitPrice;

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

        internal void SetAmount(int amount)
        {
            Amount = amount;
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
