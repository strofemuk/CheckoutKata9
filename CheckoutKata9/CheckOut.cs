using CheckoutKata9.PricingRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata9
{
    public class CheckOut : CheckoutKata9.ICheckOut
    {
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
                Product = GetProduct(sku)
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
                total += sku.pricingRule.SubTotal(BaggedItems, GetProduct(sku.sku).UnitPrice);
            }

            return decimal.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private Product GetProduct(char sku)
        {
            switch (sku)
            {
                case 'A': 
                    return new Product() { SKU = 'A', Description = "Drink", UnitPrice = 0.50M };
                case 'B':
                    return new Product() { SKU = 'B', Description = "Bread", UnitPrice = 0.30M };
                case 'C':
                    return new Product() { SKU = 'C', Description = "Bagel", UnitPrice = 0.20M };
                case 'D':
                    return new Product() { SKU = 'D', Description = "Toast", UnitPrice = 0.15M };
                case 'E':
                    return new Product(UnitOfMeasure.Pound) { SKU = 'E', Description = "Steak", UnitPrice = 6.99M };
                default:
                    throw new ArgumentException(string.Format("SKU {0} does not exist! ", sku));
            }
        }
    }
}
