using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9.PricingRules
{
    public class BasePricingRule
    {
        public BasePricingRule(char applyToSKU)
        {
            ApplyToSKU = applyToSKU;
        }

        public char ApplyToSKU { get; private set; }

        public virtual void CalculatePrice(List<BaggedItem> baggedItems)
        {
            baggedItems.Where(item => item.Product.SKU == ApplyToSKU)
                .ToList()
                .ForEach(item => {
                    item.UnitTotal = (item.Product.UnitPrice * item.Product.UnitQuantity);
                    item.PricingRule = this.ToString();
                });
        }

        public override string ToString()
        {
            return " ";
        }
    }
}
