using Entities;
using System;
using System.Linq;

namespace Services
{
    public class CartService
    {
        public void AddItemToCart(Cart cart, CartItem item)
        {
            var existingItem = cart.Items.FirstOrDefault(i => i.CarId == item.CarId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }
        }

        public void RemoveItemFromCart(Cart cart, int carId)
        {
            var itemToRemove = cart.Items.FirstOrDefault(i => i.CarId == carId);

            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
            }
        }

        public void UpdateCartItemQuantity(Cart cart, int carId, int newQuantity)
        {
            var itemToUpdate = cart.Items.FirstOrDefault(i => i.CarId == carId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = newQuantity;
            }
        }

        public void ClearCart(Cart cart)
        {
            cart.Items.Clear();
        }

        public decimal CalculateCartTotal(Cart cart)
        {
            decimal total = 0;

            foreach (var item in cart.Items)
            {
                total += item.DailyPrice * item.Quantity;
            }

            return total;
        }

        public void ProcessPayment(Cart cart, PaymentInfo paymentInfo)
        {
            // Ödeme işlemi gerçekleştiriliyor
            Console.WriteLine($"Ödeme işlemi gerçekleştirildi: {CalculateCartTotal(cart)} TL tutarında.");

            // Burada gerçek bir ödeme işlemi gerçekleştirilmesi gerekiyorsa, uygun entegrasyon yapılmalıdır.
            // Örneğin, bir ödeme ağ geçidi API'si kullanılabilir.
        }
    }
}
