using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace VendingMachine
{
    [TestClass]
    public class BrainTest
    {
        // Coin Weight specifications from www.usmint.gov/about_the_mint/?action=coin_specifications

        //As a vendor
        //I want a vending machine that accepts coins
        //So that I can collect money from the customers

        [TestMethod]
        public void DefaultDisplayShouldBeINSERTCOIN()
        {
            Brain o = new Brain();
            string actual = o.Display;
            //Initial message = "INSERT COIN"
            string expected = "INSERT COIN";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldUnderstandANickel()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            Brain o = new Brain();
            int coinValue = o.EvaluateCoinValueByWeightOfCoinInMilligrams(5000);
            int expected = 5; // cents
            Assert.AreEqual(expected, coinValue);
        }

        [TestMethod]
        public void ShouldUnderstandADime()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            Brain o = new Brain();
            int coinValue = o.EvaluateCoinValueByWeightOfCoinInMilligrams(2268);
            int expected = 10; // cents
            Assert.AreEqual(expected, coinValue);
        }

        [TestMethod]
        public void ShouldUnderstandAQuarter()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            Brain o = new Brain();
            int coinValue = o.EvaluateCoinValueByWeightOfCoinInMilligrams(5670);
            int expected = 25; // cents
            Assert.AreEqual(expected, coinValue);
        }
    }
}
