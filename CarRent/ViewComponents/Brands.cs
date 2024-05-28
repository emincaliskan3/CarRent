using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRent.ViewComponents
{
    public class Brands : ViewComponent
    {
        private readonly DatabaseContext _context;

        public Brands(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Brands.ToListAsync()); // geriye view ekranı dönüyoruz
        }
    }
}
