using CheckoutKata9.PricingRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9
{
    public class BaggedItem
    {
        public Product Product { get; set; }
        public float UnitTotal { get; set; }
        public string PricingRule { get; set; }
    }
}
