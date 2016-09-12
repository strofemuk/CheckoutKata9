using System;
namespace CheckoutKata9
{
    public interface ICheckOut
    {
        System.Collections.Generic.List<IItem> BaggedItems { get; }
        System.Collections.Generic.List<PricingRules.IPricingRule> PricingRules { get; }
        string Receipt();
        void Scan(char sku);
        decimal Scan(string skus);
        decimal Total();
    }
}
