﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{

    public class CoinEvaluator
    {
        public string CoinReturnState = "";
        // Coin Weight specifications from www.usmint.gov/about_the_mint/?action=coin_specifications
        // TODO - handle weight variances
        // TODO - use Thickness as a part of evaluation
        // TODO - use Edge topography as part of evaluation
        public int EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(int weightOfCoinInGrams, double diameterOfCoinInMillemeters)
        {
            CoinReturnState = "DEPOSITED";
            if (weightOfCoinInGrams == 5000 && diameterOfCoinInMillemeters == 21.21)
                return 5; // A nickel == 5 cents
            else if (weightOfCoinInGrams == 2268 && diameterOfCoinInMillemeters == 17.91)
                return 10; // A dime == 10 cents
            else if (weightOfCoinInGrams == 5670 && diameterOfCoinInMillemeters == 24.26)
                return 25; // A quarter == 25 cents
            else
            {
                CoinReturnState = "RETURNED";
                return 0;
            }
        }
    }

    public class Brain
    {
        //As a vendor
        //I want a vending machine that accepts coins
        //So that I can collect money from the customers
        int totalValue = 0;  // in cents
        bool insufficentMoneyFlag = false;
        bool justPurchased = false;

        bool soldOut = false;
        decimal currentPrice = 0;
        public int Refunded = 0;
        public int Drink_Inventory = 0; // TODO add call to inventory checker
        public int Chip_Inventory = 0; // TODO add call to inventory checker
        public int Candy_Inventory = 0; // TODO add call to inventory checker
        public bool ExactChangeOnly = false;

        public void ClearValue()
        {
            totalValue = 0;
        }

        public string Display
        {
            get 
            {
                if( insufficentMoneyFlag )
                {
                    insufficentMoneyFlag = false;
                    string message = "PRICE " + (currentPrice/100).ToString("C");
                    return message;
                }
                else if (soldOut)
                {
                    soldOut = false;
                    return "SOLD OUT";
                }
                else if (ExactChangeOnly)
                {
                    ExactChangeOnly = false;
                    return "EXACT CHANGE ONLY";
                }
                else if (justPurchased)
                {
                    justPurchased = false;
                    return "THANK YOU";
                }
                else if (totalValue == 0)
                {
                    return "INSERT COIN";
                }
                else if (totalValue > 99)
                {
                    decimal tempValue = (decimal)totalValue / 100;
                    return tempValue.ToString("N2"); // That was much more work than it should have been
                }
                else
                    return totalValue.ToString();
            }
        }

        public int TotalValue
        {
            get { return totalValue; }
            set { totalValue = value; }
        }


        public int AddValue(int coinValue)
        {
            totalValue += coinValue;
            return totalValue;
        }

        public void SelectProduct(string productName, int priceInCents)
        {
            if (ProductAvailable(productName))
            {
                if (priceInCents > totalValue)
                {
                    insufficentMoneyFlag = true;
                    currentPrice = priceInCents;
                }
                else
                {
                    justPurchased = true;
                    currentPrice = 0;
                    totalValue -= priceInCents;
                    if (totalValue > 0)
                        Refunded = totalValue;
                    totalValue = 0;
                    //TODO Figure out what to do if unable to refund money
                    //TODO Call coin dispensor with Refund Command
                    //TODO Call Dispence Product on ProductDispensor
                }
            }
            else
            {
                soldOut = true;
            }
        }

        public bool ProductAvailable(string productName)
        {
            if (productName == "Drink" && Drink_Inventory > 0)
                return true;
            else if (productName == "Chips" && Chip_Inventory > 0)
                return true;
            else if (productName == "Candy" && Candy_Inventory > 0)
                return true;
            else
                return false;
        }


       

        public void CoinReturn()
        {
            Refunded = totalValue;
            totalValue = 0;
            //TODO Call coin dispensor with Refund Command
        }


 

    }
}
