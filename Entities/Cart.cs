using System;
using System.Collections.Generic;

namespace Entities
{
    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsCheckedOut { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        totalPrice += item.TotalPrice;
                    }
                }
                return totalPrice;
            }
        }

        public Cart()
        {
            Items = new List<CartItem>();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }
    }
}
