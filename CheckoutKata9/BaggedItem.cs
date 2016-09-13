using CheckoutKata9.PricingRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9
{
    public class BaggedItem : IItem
    {
        public Product Product { get; set; }
        public decimal UnitTotal { get; set; }
    }
}
