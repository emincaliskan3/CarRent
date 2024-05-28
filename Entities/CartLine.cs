using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CartLine
    {
        public Car Car { get; set; }
        public int Quantity { get; set; }
    }
}
