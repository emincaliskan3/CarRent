using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), StringLength(150), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Ürün Açıklaması"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Anasayfada Göster")]
        public bool IsHome { get; set; }
        [Display(Name = "Stok"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public int Stock { get; set; }
        [Display(Name = "Fiyat"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public decimal Price { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Kategori Adı")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public virtual Category? Category { get; set; }
        [Display(Name = "Marka Adı")]
        public int? BrandId { get; set; }
        [Display(Name = "Marka")]
        public virtual Brand? Brand { get; set; }

        [Display(Name = "Modeli")]
        public string? Model { get; set; }
        [Display(Name = "Motor Gücü")]
        public int? MotorGucu { get; set; }
        [Display(Name = "Vites")]
        public string? GearName { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<User>? Users { get; set; }

    }
}
