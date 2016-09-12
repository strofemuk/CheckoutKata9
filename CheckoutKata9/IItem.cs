using System;
namespace CheckoutKata9
{
    public interface IItem
    {
        string PricingRuleDescription { get; set; }
        Product Product { get; set; }
        decimal UnitTotal { get; set; }
    }
}
