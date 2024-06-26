﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Soyad"), StringLength(50)]
        public string Surname { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Şifre"), StringLength(50)]
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        [Display(Name = "Adres")]
        public string Address {  get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }

    }
}