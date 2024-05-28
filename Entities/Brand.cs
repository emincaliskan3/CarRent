using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Brand
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı"), StringLength(150), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Üretim Yeri"), StringLength(150), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Country { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        public virtual List<Car>? Cars { get; set; }

    }
}
