using System;
namespace CheckoutKata9.PricingRules
{
    public interface IPricingRule
    {
        char ApplyToSKU { get; }
        decimal SubTotal(System.Collections.Generic.List<IItem> baggedItems, decimal normalPrice);
        string ToString();
    }
}
