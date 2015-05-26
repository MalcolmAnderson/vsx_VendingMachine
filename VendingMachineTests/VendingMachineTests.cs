using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace VendingMachine
{
    [TestClass]
    public class CoinEvaluatorTests_CoinDetection
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

        [TestMethod]
        public void BadQuarterShouldBeReturned()
        {
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(5670, 22.21); // quarter weight, nickel diameter
            Assert.AreEqual(o.CoinReturnState, "RETURNED");
        }

        [TestMethod]
        public void GoodQuarterShouldBeKept()
        {
            int coinValue = o.EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(5670, 24.26);
            Assert.AreEqual(o.CoinReturnState, "DEPOSITED");
        }


    }

    [TestClass]
    public class BrainTests_AcceptCoinsAndUpdateDisplay
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

        [TestMethod]
        public void ConfirmZeroValueToStart()
        {
            Assert.AreEqual(0, o.TotalValue);
        }

        [TestMethod]
        public void AddOneOfEachCoin()
        {
            o.AddValue(5);
            Assert.AreEqual("5", o.Display);
            o.AddValue(10);
            Assert.AreEqual("15", o.Display);
            o.AddValue(25);
            Assert.AreEqual("40", o.Display);
            int expected = 40;
            Assert.AreEqual(expected, o.TotalValue);
        }

        [TestMethod]
        public void MoreThanADollarShouldAddADecimalToDisplay()
        {
            o.AddValue(10);
            o.AddValue(25);
            o.AddValue(25);
            Assert.AreEqual("60", o.Display);
            o.AddValue(25);
            Assert.AreEqual("85", o.Display);
            o.AddValue(25);
            Assert.AreEqual("1.10", o.Display);
            int expected = 110;
            Assert.AreEqual(expected, o.TotalValue);
        }
    }


    [TestClass]
    public class BrainTests_ProductSelection
    {
        // Ready message standardized on "INSERT COIN" rather than "INSERT COINS" - Check with Product Owner

        Brain o;
        [TestInitialize]
        public void Setup()
        {
            o = new Brain();
            o.Drink_Inventory = 1;
        }

        [TestMethod]
        public void SelectPopWithNoMoneyShouldWorkAsExpected()
        {
            // zero cash, select pop
            o.SelectProduct("Drink", 100);
            Assert.AreEqual("PRICE $1.00", o.Display, "Should have displayed price");
            Assert.AreEqual("INSERT COIN", o.Display, "Should have displayed INSERT COIN");
        }

        // Add dispensor and check state on success and failure

        [TestMethod]
        public void SelectPopShouldWorkAsExpected()
        {
            o.AddValue(110);
            o.SelectProduct("Drink", 100);
            o.ClearValue();
            Assert.AreEqual("THANK YOU", o.Display);
            Assert.AreEqual("INSERT COIN", o.Display);
            Assert.AreEqual(0, o.TotalValue);
        }
    }



    [TestClass]
    public class BrainTests_MakeChange
    {
        // Ready message standardized on "INSERT COIN" rather than "INSERT COINS" - Check with Product Owner

        Brain o;
        [TestInitialize]
        public void Setup()
        {
            o = new Brain();
            o.Chip_Inventory = 1;
        }

        [TestMethod]
        public void SelectChipsWithExactChange()
        {
            o.AddValue(110);
            o.SelectProduct("Chips", 50);
            o.ClearValue();
            Assert.AreEqual("THANK YOU", o.Display);
            Assert.AreEqual("INSERT COIN", o.Display);
            Assert.AreEqual(0, o.TotalValue);
        }

        [TestMethod]
        public void SelectChipsWithExtraMoney()
        {
            o.AddValue(65);
            o.SelectProduct("Chips", 50);
            o.ClearValue();
            Assert.AreEqual("THANK YOU", o.Display);
            Assert.AreEqual("INSERT COIN", o.Display);
            Assert.AreEqual(o.Refunded, 15, "Refunded should have been 15 cents");
            Assert.AreEqual(0, o.TotalValue, "Total should have been set to zero");
        }
    }



    [TestClass]
    public class BrainTests_ReturnCoins
    {
        Brain o;
        [TestInitialize]
        public void Setup()
        {
            o = new Brain();
        }


        [TestMethod]
        public void PressCoinReturnWithMoneyEntered()
        {
            o.AddValue(65);
            o.CoinReturn();
            Assert.AreEqual("INSERT COIN", o.Display);
            Assert.AreEqual(o.Refunded, 65, "Refunded should have been 65 cents");
            Assert.AreEqual(0, o.TotalValue, "Total should have been set to zero");
        }
    }



    [TestClass]
    public class BrainTests_SoldOut
    {
        Brain o;
        [TestInitialize]
        public void Setup()
        {
            o = new Brain();
        }

        // TODO Selections without money should also display sold out

        [TestMethod]
        public void SelectionWithMoneyShouldDisplaySoldOutAndThenAmount()
        {
            o.Chip_Inventory = 0;
            o.AddValue(110);
            o.SelectProduct("Chips", 50);
            Assert.AreEqual("SOLD OUT", o.Display);
            Assert.AreEqual("1.10", o.Display);
        }

        [TestMethod]
        public void SelectionWithOutMoneyShouldDisplaySoldOutAndThenINSERTCOIN()
        {
            o.Chip_Inventory = 0;
            o.SelectProduct("Chips", 50);
            Assert.AreEqual("SOLD OUT", o.Display);
            Assert.AreEqual("INSERT COIN", o.Display);
        }

    }

}
