using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;

namespace Domain.Model
{
    public class Item
    {
        private ICollection<ItemSurvey> _surveys { get; set; }
        
        private IPricingStrategy _pricingStrategy;
        
        public int Id { get; set; }

        public Item(IPricingStrategy strategy)
        {
            this._surveys = new HashSet<ItemSurvey>();
            this._pricingStrategy = strategy;
        }

        public string Name { get; set; }

        public int SurveyCount
        {
            get { return _surveys.Count; }
        }

        public double? Price
        {
            get
            {
                if (this._surveys != null && this._surveys.Count > 0)
                    return this._pricingStrategy.GetPrice(this._surveys.Select(x => x.Price).Min());
                else
                    return null;
            }
        }

        public void AddSurvey(ItemSurvey survey)
        {
            if (this._surveys.Count > 0)
            {
                double average = this._surveys.Select(x => x.Price).Average();
                double priceFiftyPercentOfAverage = average * 0.5;
                double priceMoreThanFiftyPercentOfAveragePrice = average + priceFiftyPercentOfAverage;
                if (survey.Price >= priceFiftyPercentOfAverage && survey.Price < priceMoreThanFiftyPercentOfAveragePrice)
                    this._surveys.Add(survey);
            }
            else
            {
                this._surveys.Add(survey);
            }
        }
    }
}
