using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Controllers
{
    public class CarsController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public CarsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var years = await _dbContext.Cars.Select(c => c.Model).Distinct().ToListAsync();
            var brands = await _dbContext.Cars.Select(c => c.Brand.Name).Distinct().ToListAsync();
            var categories = await _dbContext.Cars.Select(c => c.Category.Name).Distinct().ToListAsync();
            var prices = await _dbContext.Cars.Select(c => c.Price).Distinct().ToListAsync();

            ViewBag.Models = years ?? new List<string>();
            ViewBag.Brands = brands ?? new List<string>();
            ViewBag.Categories = categories ?? new List<string>();
            ViewBag.Prices = prices ?? new List<decimal>();

            var databaseContext = _dbContext.Cars
                                            .Include(p => p.Category)
                                            .Include(p => p.Brand);

            return View(await databaseContext.ToListAsync());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var model = await _dbContext.Cars
                .Where(x => x.Id == id && x.IsActive)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Comments)
                .ThenInclude(c => c.User) // Yorumlarla birlikte kullanıcı bilgilerini de dahil et
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            // Kullanıcının oturum durumunu ViewBag'e ekleyin
            ViewBag.IsLoggedIn = HttpContext.Session.GetInt32("IsLoggedIn") == 1;

            // Yorumları ViewBag'e ekleyin
            ViewBag.Comments = model.Comments ?? new List<Comment>();

            return View(model);
        }

        [HttpPost]
       
        public ActionResult Comment(Comment data)
        {
            // Kullanıcı ID'sini oturumdan alın
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Kullanıcı oturum açmamışsa hata döndür
                return Unauthorized();
            }

            data.UserId = userId.Value; // Yorum nesnesine kullanıcı ID'sini atayın
            data.Date = DateTime.Now;

            _dbContext.Comments.Add(data);
            _dbContext.SaveChanges();

            return RedirectToAction("Detail", new { id = data.CarId });
        }


        public async Task<IActionResult> Search(string model, string brand, string category, string gearname, string q)
        {
            var query = _dbContext.Cars.Include(c => c.Brand).Include(c => c.Category).AsQueryable();

            if (!string.IsNullOrEmpty(model) && model != "default")
            {
                query = query.Where(c => c.Model.ToLower() == model.ToLower());
            }

            if (!string.IsNullOrEmpty(brand) && brand != "default")
            {
                query = query.Where(c => c.Brand.Name.ToLower() == brand.ToLower());
            }

            if (!string.IsNullOrEmpty(category) && category != "default")
            {
                query = query.Where(c => c.Category.Name.ToLower() == category.ToLower());
            }

            if (!string.IsNullOrEmpty(gearname) && gearname != "default")
            {
                query = query.Where(c => c.GearName.ToLower() == gearname.ToLower());
            }

            if (!string.IsNullOrEmpty(q))
            {
                var lowercaseQuery = q.ToLower();
                query = query.Where(c => c.Model.ToLower().Contains(lowercaseQuery) ||
                                         c.Brand.Name.ToLower().Contains(lowercaseQuery) ||
                                         c.Category.Name.ToLower().Contains(lowercaseQuery) ||
                                         c.GearName.ToLower().Contains(lowercaseQuery));
            }

            var cars = await query.ToListAsync();

            return View("Search", cars);
        }
    }
}
