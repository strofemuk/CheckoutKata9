using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9.PricingRules
{
    public class XForN : IPricingRule
    {
        public XForN(char applyToSKU, int quantity, decimal specialPrice) 
        {
            ApplyToSKU = applyToSKU;
            Quantity = quantity;
            SpecialPrice = specialPrice;
        }

        public char ApplyToSKU { get; private set; }

        public int Quantity { get; private set; }
        public decimal SpecialPrice { get; private set; }
        
        public Decimal SubTotal(List<IItem> baggedItems, decimal normalPrice)
        {
            int countOfItems = baggedItems.Count(g => g.Product.SKU == this.ApplyToSKU);

            int countOfSpecialPricing = countOfItems / Quantity;
            int countOfNormalPricing = countOfItems % Quantity;

            return (countOfSpecialPricing * SpecialPrice) + (countOfNormalPricing * normalPrice);
        }

        public override string ToString()
        {
            return string.Format("{0} for {1} sale.", Quantity, SpecialPrice.ToString("c2"));
        }
    }
}
