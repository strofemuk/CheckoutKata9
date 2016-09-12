using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9
{
    public class Product
    {
        public Product(UnitOfMeasure uom = UnitOfMeasure.Each)
        {
            UoM = uom;
            if (UoM != UnitOfMeasure.Each)
            {
                Console.Write("How many {0} are being bought:", UoM);
                UnitQuantity = decimal.Parse(Console.ReadLine());
            }
            else
            {
                UnitQuantity = 1;
            }
        }

        public string Description { get; set; }
        public char SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitQuantity { get; set; }

        public UnitOfMeasure UoM { get; set; }

    }

    public enum UnitOfMeasure {
        Each = 0,
        Pound = 1,
        Kilo = 2,
        Ton = 4,
    }
}
