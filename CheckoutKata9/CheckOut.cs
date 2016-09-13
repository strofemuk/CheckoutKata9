using CheckoutKata9.PricingRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9
{
    public class CheckOut : ICheckOut
    {
        private ProductRepository _products = new ProductRepository();

        public CheckOut(List<PricingRules.IPricingRule> pricingRules)
        {
            PricingRules = pricingRules;
            BaggedItems = new List<IItem>();
        }

        public List<IItem> BaggedItems { get; private set; }
        public List<PricingRules.IPricingRule> PricingRules { get; private set; }
        
        public void Scan(char sku)
        {
            BaggedItems.Add(new BaggedItem() { 
                Product = _products[sku]
            });
        }

        public decimal Scan(string skus)
        {
            BaggedItems.Clear();
            foreach (char sku in skus)
            {
                Scan(sku);
            }
            return this.Total();
        }

        public decimal Total() 
        { 
            decimal total = 0.0M;

            var skuCounts = from b in BaggedItems
                            group b by b.Product.SKU into g
                            join rule in PricingRules on g.Key equals rule.ApplyToSKU
                            select new { sku = g.Key, count = g.Count(), pricingRule = rule };

            foreach (var sku in skuCounts)
            {
                total += sku.pricingRule.SubTotal(BaggedItems, _products[sku.sku].UnitPrice);
            }

            return decimal.Round(total, 2, MidpointRounding.AwayFromZero);
        }
    }
}
