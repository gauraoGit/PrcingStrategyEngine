﻿using Domain.Interfaces;

namespace Application.PricingStrategy
{
    class HighSupplyLowDemandStrategy : IPricingStrategy
    {
        public double GetPrice(double chosenPrice)
        {
            return chosenPrice - (chosenPrice * 0.05);
        }
    }
}