using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9.PricingRules
{
    class XForN : BasePricingRule
    {
        public XForN(char applyToSKU, int quantity, float specialPrice) 
            : base(applyToSKU)
        {
            Quantity = quantity;
            SpecialPrice = specialPrice;
        }

        public int Quantity { get; private set; }
        public float SpecialPrice { get; private set; }
        
        public override void CalculatePrice(List<BaggedItem> baggedItems)
        {
            int countOfItems = baggedItems.Count(g => g.Product.SKU == this.ApplyToSKU);

            if (countOfItems >= Quantity)
            {
                baggedItems.Where(inBag => inBag.Product.SKU == this.ApplyToSKU)
                    .ToList()
                    .ForEach(item => {
                        item.UnitTotal = (this.SpecialPrice / this.Quantity);
                        item.PricingRule = this.ToString();
                    });
            }
            else
            {
                base.CalculatePrice(baggedItems);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} for {1} sale.", Quantity, SpecialPrice.ToString("c2"));
        }
    }
}
