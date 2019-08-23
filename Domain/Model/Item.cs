using Domain.Interfaces;

namespace Domain.Model
{
    public class Item
        {
            private IPricingStrategy _pricingStrategy;

            public Item(IPricingStrategy pricingStrategy)
            {
                this._pricingStrategy = pricingStrategy;
            }
            
            public string Name { get; set; }

            public double Price { get; private set; }

            public double GetPrice(double surveyPrice)
            {
                return _pricingStrategy.GetPrice(this.Price);
            }
            
        }
}
