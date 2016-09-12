using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9.PricingRules
{
    public class BasePricingRule : PricingRules.IPricingRule
    {
        public BasePricingRule(char applyToSKU)
        {
            ApplyToSKU = applyToSKU;
        }

        public char ApplyToSKU { get; private set; }

        public virtual decimal SubTotal(List<IItem> baggedItems, decimal normalPrice)
        {
            int countOfItems = baggedItems.Count(g => g.Product.SKU == this.ApplyToSKU);

            return countOfItems * normalPrice;
        }

        public override string ToString()
        {
            return " ";
        }
    }
}
