using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum OrderStatus
    {
        Beklemede,
        Onaylandı,
        Onaylanmadı
    }
    public class Order : IEntity
    {
        public int Id { get; set; }

        // Müşteri bilgileri
        [Display(Name = "Ad"), StringLength(50), Required]
        public string Name { get; set; }
        [Display(Name = "Soyad"), StringLength(50), Required]
        public string Surname { get; set; }
        [StringLength(50), Required]
        public string Email { get; set; }
        [Display(Name = "Telefon"), StringLength(15)]
        public string? Phone { get; set; }
        [Display(Name = "Adres"), StringLength(500)]
        public string? Address { get; set; } // Making Address nullable
        [Display(Name = "Sipariş Bilgileri")]
        public string? OrderDetail { get; set; } // Making OrderDetail nullable
        [Display(Name = "Sipariş Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "CVV"), StringLength(3), Required]
        public string CVV { get; set; }
        [Display(Name = "Kart Numarası"), StringLength(16), Required]
        public string CardNo { get; set; }

        // Kiralama bilgileri
        [Display(Name = "Kiralama Başlangıç Tarihi")]
        public DateTime RentStartDate { get; set; }
        [Display(Name = "Kiralama Bitiş Tarihi")]
        public DateTime RentEndDate { get; set; }
        [Display(Name = "Araç ID")]
        public int CarId { get; set; }
        [Display(Name = "Araç")]
        public virtual Car? Car { get; set; }
        [Display(Name = "Kullanıcı ID")]
        public int UserId { get; set; }

        [Display(Name = "Marka")]
        public string CarName { get; set; } // Add this property
       
        public OrderStatus Status { get; set; }
    }
}
