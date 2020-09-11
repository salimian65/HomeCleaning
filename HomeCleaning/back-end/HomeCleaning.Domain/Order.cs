using System;

namespace HomeCleaning.Domain
{
    public class Order : Entity
    {
        public OrderStatus OrderStatus { get; set; }

        public Address Address { get; set; }

        public DateTime ScheduledTime { get; set; }

        public DateTime Registered { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Tax { get; set; }
    }
}