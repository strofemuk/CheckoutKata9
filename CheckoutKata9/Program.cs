using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PricingRules.BasePricingRule> pricingRules = new List<PricingRules.BasePricingRule>();
            pricingRules.Add(new PricingRules.XForN('A',3, 1.30f));
            pricingRules.Add(new PricingRules.XForN('B',2, 0.45f));
            pricingRules.Add(new PricingRules.BasePricingRule('C'));
            pricingRules.Add(new PricingRules.BasePricingRule('D'));
            pricingRules.Add(new PricingRules.BasePricingRule('E'));

            CheckOut checkOut = new CheckOut(pricingRules);
            checkOut.Scan("CDBAE"); 

            Console.WriteLine("The total for CDBA is: {0}", checkOut.Total().ToString("c2"));

            Console.WriteLine();
            Console.WriteLine(checkOut.Receipt());

            checkOut.Scan("DABABA");  //should be 1.90
            Console.WriteLine("The total for DABABA is: {0}", checkOut.Total().ToString("c2"));

            Console.WriteLine();
            Console.WriteLine(checkOut.Receipt());

            Console.ReadKey();
        }
    }
}
