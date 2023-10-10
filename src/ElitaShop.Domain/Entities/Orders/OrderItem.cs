﻿namespace ElitaShop.Domain.Entities.Orders
{

    public class OrderItem : Auditable
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
