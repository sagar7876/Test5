﻿using ElitaShop.Domain.Entities.Carts;
using ElitaShop.Domain.Entities.Orders;
using ElitaShop.Domain.Entities.Users;

namespace ElitaShop.Domain.Entities.Products
{
    public class Product : Auditable
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public bool IsShop { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string? ProductImage { get; set; }

        public User User { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
