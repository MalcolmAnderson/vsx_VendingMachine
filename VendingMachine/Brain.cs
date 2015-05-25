using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
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

        // Coin Weight specifications from www.usmint.gov/about_the_mint/?action=coin_specifications
        // TODO - handle weight variances
        // TODO - use Diameter as part of evaluation
        // TODO - use Thickness as a part of evaluation
        // TODO - use Edge topography as part of evaluation
        public int EvaluateCoinValueByWeightOfCoinInMilligrams(int weightOfCoinInGrams)
        {
            if (weightOfCoinInGrams == 5000)
                return 5; // A nickel == 5 cents
            else if (weightOfCoinInGrams == 2268)
                return 10; // A dime == 10 cents
            else
                return 0;
        }

    }
}
