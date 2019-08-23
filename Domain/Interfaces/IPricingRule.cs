using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPricingRule
    {
        double GetSurveyPrice(string productName, List<double> surveyList);
    }
}
