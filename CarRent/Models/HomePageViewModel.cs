using Entities;
using Microsoft.Extensions.Hosting;

namespace CarRent.Models
{
    public class HomePageViewModel
    {
        
        public List<Category>? Categories { get; set; }
        public List<Car>? Cars { get; set; }
        public List<Post>? Posts { get; set; }
		public List<Brand>? Brands { get; set; }
        public List<Comment>? Comments { get; set; }



    }
}
