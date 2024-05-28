using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Controllers
{
    public class BrandsController : Controller
    {
        private readonly DatabaseContext _context;

        public BrandsController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return BadRequest();

            }
            var brands = _context.Brands.Include(p => p.Cars).FirstOrDefault(k => k.Id == id);
            if (brands == null)
                return NotFound();

            return View(brands);
        }
    }
}
