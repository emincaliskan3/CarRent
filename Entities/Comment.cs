using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Display(Name = "Yorum")]
        public string Contents { get; set; }
        [Display(Name = "Araç")]
        public int CarId { get; set; }
        public virtual Car? Car { get; set; }
        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Popüler Yorum")]
        public bool IsPopular { get; set; }
    }
}
