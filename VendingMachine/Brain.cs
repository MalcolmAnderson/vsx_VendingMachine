﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{

    public class CoinEvaluator
    {
        // Coin Weight specifications from www.usmint.gov/about_the_mint/?action=coin_specifications
        // TODO - handle weight variances
        // TODO - use Thickness as a part of evaluation
        // TODO - use Edge topography as part of evaluation
        public int EvaluateCoinValueByWeightInMilligramsAndDiameterInMillimeters(int weightOfCoinInGrams, double diameterOfCoinInMillemeters)
        {
            if (weightOfCoinInGrams == 5000 && diameterOfCoinInMillemeters == 21.21)
                return 5; // A nickel == 5 cents
            else if (weightOfCoinInGrams == 2268 && diameterOfCoinInMillemeters == 17.91)
                return 10; // A dime == 10 cents
            else if (weightOfCoinInGrams == 5670 && diameterOfCoinInMillemeters == 24.26)
                return 25; // A quarter == 25 cents
            else
                return 0;
        }
    }

    public class Brain
    {
        //As a vendor
        //I want a vending machine that accepts coins
        //So that I can collect money from the customers
        string display = "INSERT COIN";
        public string Display
        {
            get
            {
                return display;
            }
            set
            {
                display = value;
            }
        }

 

    }
}
