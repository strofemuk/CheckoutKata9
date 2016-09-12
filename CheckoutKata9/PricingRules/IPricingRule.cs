using System;
namespace CheckoutKata9.PricingRules
{
    public interface IPricingRule
    {
        char ApplyToSKU { get; }
        void CalculatePrice(System.Collections.Generic.List<IItem> baggedItems);
        string ToString();
    }
}
