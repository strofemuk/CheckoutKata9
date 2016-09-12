using System;
namespace CheckoutKata9
{
    public interface IItem
    {
        string PricingRule { get; set; }
        Product Product { get; set; }
        decimal UnitTotal { get; set; }
    }
}
