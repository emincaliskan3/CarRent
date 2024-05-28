using CarRent.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarRent.Controllers
{
    public class HomeController : Controller
    {
		private readonly DatabaseContext _context;

		public HomeController(DatabaseContext context)
		{
			_context = context;
		}

        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {
                Categories = _context.Categories.Where(p => p.IsActive && p.IsHome).ToList(),
                Cars = _context.Cars.Where(p => p.IsActive && p.IsHome).ToList(),
                Posts = _context.Posts.ToList(),
                Brands = _context.Brands.ToList(),
                Comments = _context.Comments.Include(c => c.User).Where(c => c.IsPopular).ToList()
            };

            ViewBag.TopBrands = _context.Brands.ToList();

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
