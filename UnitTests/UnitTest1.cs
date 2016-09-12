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
            Assert.AreEqual(.5M,_checkOut.Scan("A"));
            Assert.AreEqual(.8M, _checkOut.Scan("AB"));
            Assert.AreEqual(1.15M, _checkOut.Scan("CDBA"));

            Assert.AreEqual(1.00M, _checkOut.Scan("AA"));
            Assert.AreEqual(1.30M, _checkOut.Scan("AAA"));
            Assert.AreEqual(1.80M, _checkOut.Scan("AAAA"));
            Assert.AreEqual(2.30M, _checkOut.Scan("AAAAA"));
            Assert.AreEqual(2.60M, _checkOut.Scan("AAAAAA"));

            Assert.AreEqual(1.60M, _checkOut.Scan("AAAB"));
            Assert.AreEqual(1.75M, _checkOut.Scan("AAABB"));
            Assert.AreEqual(1.90M, _checkOut.Scan("AAABBD"));
            Assert.AreEqual(1.90M, _checkOut.Scan("DABABA"));
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
