using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CheckoutKata9;
using CheckoutKata9.PricingRules;


namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private List<IPricingRule> _pricingRules = new List<IPricingRule>();
        private ICheckOut _checkOut;

        [TestInitialize]
        public void TestInit()
        {
            _pricingRules.Clear();
            _pricingRules.Add(new XForN('A', 3, 1.30M));
            _pricingRules.Add(new XForN('B', 2, 0.45M));
            _pricingRules.Add(new BasePricingRule('C'));
            _pricingRules.Add(new BasePricingRule('D'));
            _pricingRules.Add(new BasePricingRule('E'));

            _checkOut = new CheckOut(_pricingRules);
        }

        [TestMethod]
        public void TestPrice()
        {
            Assert.AreEqual(_checkOut.Scan("A"), .5M);
            Assert.AreEqual(_checkOut.Scan("AB"), .8M);
            Assert.AreEqual(_checkOut.Scan("CDBA"), 1.15M);

            Assert.AreEqual(_checkOut.Scan("AA"), 1.00M);
            Assert.AreEqual(_checkOut.Scan("AAA"), 1.30M);
            Assert.AreEqual(_checkOut.Scan("AAAA"), 1.80M);
            Assert.AreEqual(_checkOut.Scan("AAAAA"), 2.30M);
            Assert.AreEqual(_checkOut.Scan("AAAAAA"), 2.60M);

            Assert.AreEqual(_checkOut.Scan("AAAB"), 1.60M);
            Assert.AreEqual(_checkOut.Scan("AAABB"), 1.75M);
            Assert.AreEqual(_checkOut.Scan("AAABBD"), 1.90M);
            Assert.AreEqual(_checkOut.Scan("DABABA"), 1.90M);
        }

        [TestMethod]
        public void TestIncremental()
        {
            Assert.AreEqual(_checkOut.Total(), 0.0M);

            _checkOut.Scan('A');
            Assert.AreEqual(_checkOut.Total(), 0.5M);

            _checkOut.Scan('B');
            Assert.AreEqual(_checkOut.Total(), 0.8M);

            _checkOut.Scan('A');
            Assert.AreEqual(_checkOut.Total(), 1.3M);

            _checkOut.Scan('A');
            Assert.AreEqual(_checkOut.Total(), 1.6M);

            _checkOut.Scan('B');
            Assert.AreEqual(_checkOut.Total(), 1.75M);
        }
    }
}
