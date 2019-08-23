using System;
using Application.PricingStrategy;
using Domain.Interfaces;

namespace Application.PricingStrategyManager
{
    public class PricingStrategyManager : IPricingStrategyManager
    {

        public IPricingStrategy GetPricingStrategy(char supply, char demand)
        {
            if ((supply == 'H' || supply == 'L') && (demand == 'H' || demand == 'L'))
            {
                if (supply == 'H' && demand == 'H')
                    return new HighSupplyHighDemandStrategy();
                if (supply == 'H' && demand == 'L')
                    return new HighSupplyLowDemandStrategy();
                if (supply == 'L' && demand == 'H')
                    return new LowSupplyHighDemandStrategy();
                if (supply == 'L' && demand == 'L')
                    return new LowSupplyLowDemandStrategy();
            }
            throw new Exception("Supply or Demand value is not correct.");
        }
    }
}