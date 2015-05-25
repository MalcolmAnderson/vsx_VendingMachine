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
    }
}
