using System;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.PricingRule
{
    public class PricingRule: IPricingRule
    {
       
       
        public double GetSurveyPrice(string productName, List<double> surveyList)
        {
            if (surveyList == null)
                throw new ArgumentNullException("surveyList", "Should not be null.");
            if (string.IsNullOrEmpty(productName))
                throw new ArgumentNullException("productName", "Should not be null or empty.");

            double average = surveyList.Average();
            double priceFiftyPercentOfAverage = average * 0.5;
            double priceMoreThanFiftyPercentOfAveragePrice = average + priceFiftyPercentOfAverage;
            
            //Get prices less than 50% of average and less than 
            var validListOfPrices = surveyList
                .Where(x => x >= priceFiftyPercentOfAverage && x < priceMoreThanFiftyPercentOfAveragePrice)
                .OrderBy(x=>x);
            return validListOfPrices.Min();
        }
    }
}
