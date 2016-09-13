using System;
namespace CheckoutKata9
{
    public interface IItem
    {
        Product Product { get; set; }
        decimal UnitTotal { get; set; }
    }
}
