using Domain.Interfaces;

namespace Application.PricingStrategy
{
    class LowSupplyLowDemandStrategy : IPricingStrategy
    {
        public double GetPrice(double chosenPrice)
        {
            return chosenPrice + (chosenPrice * 0.1);
        }
    }
}