using Domain.Interfaces;

namespace Application.PricingStrategy
{
    class HighSupplyHighDemandStrategy : IPricingStrategy
    {
        public double GetPrice(double chosenPrice)
        {
            return chosenPrice;
        }
    }
}