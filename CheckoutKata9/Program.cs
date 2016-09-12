using CheckoutKata9.PricingRules;
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
            List<IPricingRule> pricingRules = new List<IPricingRule>();
            pricingRules.Add(new XForN('A', 3, 1.30M));
            pricingRules.Add(new XForN('B', 2, 0.45M));
            pricingRules.Add(new BasePricingRule('C'));
            pricingRules.Add(new BasePricingRule('D'));
            pricingRules.Add(new BasePricingRule('E'));


            ICheckOut checkOut = new CheckOut(pricingRules);
            checkOut = new CheckOut(pricingRules);

            Console.WriteLine("Scan ABAB:  {0}", checkOut.Scan("ABAB"));

            Console.ReadKey();
        }
    }
}
