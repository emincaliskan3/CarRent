using System;

namespace Entities
{
    public class CartItem
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public decimal DailyPrice { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
        public int TotalDays => (int)(RentEndDate - RentStartDate).TotalDays;
        public decimal TotalPrice => TotalDays * DailyPrice;
        public int Quantity { get; set; } // Quantity özelliği eklendi
    }
}
