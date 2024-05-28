using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using Entities;
using Data;
using System.Net;

namespace CarRent.Controllers
{
    public class CartController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public CartController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> RentCar(int carId, DateTime startDate, DateTime endDate)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null)
            {
                return NotFound();
            }

            // Kiralama süresini hesapla
            TimeSpan rentalPeriod = endDate - startDate;

            // Toplam ücreti hesapla
            decimal totalPrice = car.Price * (decimal)rentalPeriod.TotalDays;

            // Kiralama işlemi için gerekli bilgileri ViewBag'e ekle
            ViewBag.CarId = carId;
            if (car.Brand != null) // Null kontrolü
            {
                ViewBag.CarName = $"{car.Brand.Name} {car.Name}";
            }
            else
            {
                ViewBag.CarName = $"{car.Name}";
            }
            ViewBag.DailyPrice = car.Price;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.TotalPrice = totalPrice; // Toplam ücreti ViewBag'e ekle

            return View("RentCarDetails");
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmRent(int carId, DateTime startDate, DateTime endDate, string cardNo, string cvv)
        {
            // Retrieve user information from session
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _dbContext.Users.FindAsync(userId);
            if (userId == null || user == null)
            {
                return Unauthorized(); // User not authenticated
            }

            // Retrieve car information
            var car = await _dbContext.Cars
                .Include(c => c.Brand) // Include the brand information
                .FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null)
            {
                return NotFound(); // Car not found
            }

            // Create order
            var order = new Order
            {
                CarId = car.Id,
                UserId = userId.Value,
                RentStartDate = startDate,
                RentEndDate = endDate,
                CardNo = cardNo,
                CVV = cvv,
                Address = user.Address,
                Email = user.Email, // Populate with user's email
                Name = user.Name, // Populate with user's first name
                Surname = user.Surname, // Populate with user's last name
                Phone = user.Phone, // Populate with user's phone number
                CreateDate = DateTime.Now,
                 CarName = car.Brand != null ? $"{car.Brand.Name} {car.Name}" : car.Name,
                Status = OrderStatus.Beklemede

            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            

            return View("RentCarConfirmation");
        }



    }
}
