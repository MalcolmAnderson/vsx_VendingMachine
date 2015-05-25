using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace VendingMachine
{
    [TestClass]
    public class CoinEvaluatorTest_CoinDetection
    {
        // Coin Weight specifications from www.usmint.gov/about_the_mint/?action=coin_specifications

        //As a vendor
        //I want a vending machine that accepts coins
        //So that I can collect money from the customers

        CoinEvaluator o;
        [TestInitialize]
        public void Setup()
        {
            o = new CoinEvaluator();
        }

        [TestMethod]
        public void ShouldUnderstandANickel()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(5000, 21.21);
            int expected = 5; // cents
            Assert.AreEqual(expected, coinValue);
        }

        [TestMethod]
        public void ShouldUnderstandADime()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(2268, 17.91);
            int expected = 10; // cents
            Assert.AreEqual(expected, coinValue);
        }

        [TestMethod]
        public void ShouldUnderstandAQuarter()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(5670, 24.26);
            int expected = 25; // cents
            Assert.AreEqual(expected, coinValue);
        }

        [TestMethod]
        public void Should_Not_UnderstandA_Bad_Nickel()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(5000, 17.91); //nickel weight, dime diameter
            int expected = 0; // cents
            Assert.AreEqual(expected, coinValue);
        }

        [TestMethod]
        public void Should_Not_UnderstandA_Bad_Dime()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(2268, 24.26); // dime weight, quarter diameter
            int expected = 0; // cents
            Assert.AreEqual(expected, coinValue);
        }

        [TestMethod]
        public void Should_Not_UnderstandA_Bad_Quarter()
        {
            // assume that GetCoin is feed a weight in grams from a scale
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(5670, 22.21); // quarter weight, nickel diameter
            int expected = 0; // cents
            Assert.AreEqual(expected, coinValue);
        }


    }

    [TestClass]
    public class BrainTest_AcceptCoinsAndUpdateDisplay
    {
        // Coin Weight specifications from www.usmint.gov/about_the_mint/?action=coin_specifications

        //As a vendor
        //I want a vending machine that accepts coins
        //So that I can collect money from the customers

        Brain o;
        [TestInitialize]
        public void Setup()
        {
            o = new Brain();
        }

        [TestMethod]
        public void DefaultDisplayShouldBeINSERTCOIN()
        {
            string actual = o.Display;
            //Initial message = "INSERT COIN"
            string expected = "INSERT COIN";
            Assert.AreEqual(expected, actual);
        }
    }
}
