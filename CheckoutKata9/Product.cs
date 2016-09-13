using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9
{
    public class Product
    {
        public string Description { get; set; }
        public char SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitQuantity { get; set; }
        public UnitOfMeasure UoM { get; set; }
    }
}
