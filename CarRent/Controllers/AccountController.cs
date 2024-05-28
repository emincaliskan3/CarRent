using CarRent.ExtensionMethods;
using Data;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace CarRent.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(string kemail, string kpassword)
        {
            try
            {
                var kullanici = await _context.Users.FirstOrDefaultAsync(u => u.Email == kemail && u.Password == kpassword);
                if (kullanici != null)
                {
                    HttpContext.Session.SetString("kullanici", kullanici.Name);
                    HttpContext.Session.SetString("soyad", kullanici.Surname);
                    HttpContext.Session.SetString("hesap", kullanici.Email);
                    HttpContext.Session.SetString("tel", kullanici.Phone);
                    HttpContext.Session.SetString("sifre", kpassword);
                    HttpContext.Session.SetInt32("IsLoggedIn", 1);
                    HttpContext.Session.SetInt32("UserId", kullanici.Id); // Kullanıcı ID'sini oturuma ekleyin
                    HttpContext.Session.SetJson("musteri", kullanici);
                    HttpContext.Session.SetString("tel", kullanici.Phone);
                    HttpContext.Session.SetString("Adres", kullanici.Address);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Giriş Başarısız!</p>";
                }
            }
            catch (Exception hata)
            {
                TempData["Message"] = hata.InnerException?.Message;
            }
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Name,Surname,Email,Phone,Password,CreateDate,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            HttpContext.Session.Remove("kullanici");
            HttpContext.Session.Remove("soyad");
            HttpContext.Session.Remove("hesap");
            HttpContext.Session.Remove("tel");
            HttpContext.Session.Remove("sifre");
            HttpContext.Session.Remove("Adres");
            HttpContext.Session.SetInt32("IsLoggedIn", 0);
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Update()
        {
            var kullaniciEmail = HttpContext.Session.GetString("hesap");
            var kullanici = await _context.Users.FirstOrDefaultAsync(u => u.Email == kullaniciEmail);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Surname,Email,Phone,Password,Address")] User updatedCustomer)
        {
            if (id != updatedCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedCustomer);
                    await _context.SaveChangesAsync();


                    HttpContext.Session.SetString("kullanici", updatedCustomer.Name);
                    HttpContext.Session.SetString("soyad", updatedCustomer.Surname);
                    HttpContext.Session.SetString("hesap", updatedCustomer.Email);
                    HttpContext.Session.SetString("tel", updatedCustomer.Phone);
                    HttpContext.Session.SetString("sifre", updatedCustomer.Password);
                    HttpContext.Session.SetString("Adres", updatedCustomer.Address);


                    return RedirectToAction("Index", "Account");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(updatedCustomer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(updatedCustomer);
        }



        private bool CustomerExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<IActionResult> KullaniciYorum()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Kullanıcı oturum açmamışsa hata döndür
                return Unauthorized();
            }

            var userComments = await _context.Comments
                .Where(c => c.UserId == userId)
                .Include(c => c.Car) // Yorum yapılan araç bilgilerini de dahil et
                .ToListAsync();

            return View(userComments);
        }
        public async Task<IActionResult> RentedCars()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // User is not authenticated
                return Unauthorized();
            }

            var rentedCars = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Car)
                    .ThenInclude(c => c.Brand)
                .ToListAsync();

            return View(rentedCars);
        }

        // Şifre sıfırlama sayfasını göster
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    // Şifre sıfırlama token'i oluşturma
                    var token = Guid.NewGuid().ToString();

                    // Token'in son kullanma tarihini belirleme (örneğin, 1 saat)
                    var tokenExpiry = DateTime.UtcNow.AddHours(1);

                    // Veritabanına token'i ve son kullanma tarihini kaydetme
                    user.ResetPasswordToken = token;
                    user.ResetPasswordTokenExpiry = tokenExpiry;
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    // Şifre sıfırlama bağlantısı
                    var resetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token }, protocol: HttpContext.Request.Scheme);

                    // E-posta gönderme
                    var fromAddress = new MailAddress("carvillarent@gmail.com", "Carvilla12");
                    var toAddress = new MailAddress(email, user.Name);
                    const string fromPassword = "rtcb nuum hygn vwnf"; // Gmail hesabınızın şifresi
                    const string subject = "Şifre Sıfırlama";
                    string body = $"Merhaba {user.Name},\n\nŞifrenizi sıfırlamak için aşağıdaki bağlantıyı kullanabilirsiniz:\n{resetLink}";

                    using (var smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("carvillarent@gmail.com", "rtcb nuum hygn vwnf")
                    })
                    {
                        using (var mailMessage = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            smtpClient.Send(mailMessage);
                        }
                    }

                    TempData["Message"] = "<p class='alert alert-success'>Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.</p>";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Bu e-posta adresi ile kayıtlı bir kullanıcı bulunamadı.</p>";
                }
            }
            return View();

        }
        

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Token ve e-posta adresi boşsa veya null ise hata göster
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Token))
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Geçersiz token veya e-posta adresi.</p>";
                    return RedirectToAction("Login");
                }

                // Kullanıcıyı e-posta adresine göre bul
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                // Kullanıcıyı bulamazsak hata göster
                if (user == null)
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Kullanıcı bulunamadı.</p>";
                    return RedirectToAction("Login");
                }

                // Token'in son kullanma tarihini kontrol et
                if (user.ResetPasswordTokenExpiry < DateTime.UtcNow)
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Şifre sıfırlama bağlantısının süresi doldu.</p>";
                    return RedirectToAction("Login");
                }

                // Token'i kontrol et
                if (user.ResetPasswordToken != model.Token)
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Geçersiz şifre sıfırlama bağlantısı.</p>";
                    return RedirectToAction("Login");
                }

                // Update the password in the database
                user.Password = model.NewPassword; // Assuming you have a property named Password in your User entity
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                TempData["Message"] = "<p class='alert alert-success'>Şifreniz başarıyla güncellendi.</p>";
                return RedirectToAction("Login");
            }
            return View(model);
        }

    }
}
    
