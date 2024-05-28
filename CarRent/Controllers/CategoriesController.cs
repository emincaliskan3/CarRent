using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly DatabaseContext _context;

		public CategoriesController(DatabaseContext context)
		{
			_context = context;
		}

		public IActionResult Index(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var category = _context.Categories
								   .Include(c => c.Cars)
								   .ThenInclude(car => car.Brand) // Brand verisini dahil ediyoruz
								   .FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}
	}
}
