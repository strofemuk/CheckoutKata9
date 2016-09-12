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

        public string Receipt()
        {
            StringBuilder returnString = new StringBuilder();

            var lines = from baggedItems in BaggedItems
                        group baggedItems by new { baggedItems.Product.SKU, LineTotal = baggedItems.UnitTotal } into baggedGroup
                        select new
                        {
                            SKU = baggedGroup.Key.SKU,
                            Description = baggedGroup.First().Product.Description,
                            UnitPrice = baggedGroup.First().Product.UoM == UnitOfMeasure.Each ? baggedGroup.Key.LineTotal : baggedGroup.First().Product.UnitPrice,
                            Quantity = baggedGroup.First().Product.UoM == UnitOfMeasure.Each ? baggedGroup.Count() : baggedGroup.First().Product.UnitQuantity,
                            UOM = baggedGroup.First().Product.UoM.ToString(),
                            Sum = baggedGroup.Sum(g => g.UnitTotal),
                            Rule = baggedGroup.First().PricingRule
                        };
            returnString.AppendLine("SKU\t| Desc\t\t| Price\t| QTY\t| UOM\t| TOTAL\t| RULE");
            returnString.AppendLine("-----------------------------------------------------------------");

            foreach (var line in lines)
            {
                returnString.AppendLine(string.Format("{0}\t| {1}\t\t| {2}\t| {3}\t| {4}\t| {5}\t|{6}",
                    line.SKU,
                    line.Description,
                    line.UnitPrice.ToString("c2"),
                    line.Quantity,
                    line.UOM,
                    line.Sum.ToString("c2"),
                    line.Rule));
            }
            returnString.AppendLine("-----------------------------------------------------------------");
            returnString.Append(string.Format("\t\t\t\t\tTotal:\t  {0}", Total().ToString("c2")));
            return returnString.ToString();
        }

        public decimal Total() 
        { 
            decimal total = 0.0M;
                        
            PricingRules.ForEach(rule =>
            {
                rule.CalculatePrice(BaggedItems);
            });

            foreach (IItem item in BaggedItems)
            {
                total += item.UnitTotal;
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
