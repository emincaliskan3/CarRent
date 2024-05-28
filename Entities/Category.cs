using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Display(Name = " Kategori Adı"), StringLength(150), Required]
        public string? Name { get; set; }
        [Display(Name = " Kategori Açıklama"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(150)]
        public string? Image { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Anasayfada Göster")]
        public bool IsHome { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public virtual List<Post>? Posts { get; set; }
        public virtual List<Car>? Cars { get; set; }
        
    }
}
