using System;
using System.Collections.Generic;
using System.Linq;
using Application.PricingRule;
using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PricingStrategyEngine.Test
{
    [TestClass]
    public class PricingRuleTests
    {
        [TestMethod]
        public void GetPriceFromSurvey_WhenOnlySingleSurvey_ShouldReturnSamePrice()
        {
            //Arrange
            List<double> surveyList = new List<double>(){129.0};
            string productName = "ssd";

            double expected = 129.0;
            //Act

            IPricingRule rule = new PricingRule();
            double actual = rule.GetSurveyPrice(productName, surveyList);

            //Assert
            Assert.AreEqual(expected, actual, "Should return single price for survey");

        }

        [TestMethod]
        public void GetPriceFromSurvey_WhenOnlyMultipleSurvey_ShouldReturnLowestPrice()
        {
            //Arrange
            List<double> surveyList = new List<double>() { 129.0 , 120.0, 121.0};
            string productName = "ssd";

            double expected = 120.0;
            //Act

            IPricingRule rule = new PricingRule();
            double actual = rule.GetSurveyPrice(productName, surveyList);

            //Assert
            Assert.AreEqual(expected, actual, "Should return lowest price from multiple surveys.");

        }

        [TestMethod]
        public void GetPriceFromSurvey_WhenOnlyMultipleSurvey_ShouldReturnLowestPriceGreaterThan50PerOfAverage()
        {
            //Arrange
            List<double> surveyList = new List<double>() { 10.0, 11, 12, 3 };
            string productName = "ssd";
            double fiftyPerOfAverage = surveyList.Average()*0.5;
            double expected = 10.0;
            //Act

            IPricingRule rule = new PricingRule();
            double actual = rule.GetSurveyPrice(productName, surveyList);

            //Assert
            Assert.IsFalse(actual < fiftyPerOfAverage, "Prices less than 50% of average price are treated as promotion and not considered.");
            Assert.AreEqual(expected, actual, "Should return lowest price but should be greater than from multiple surveys.");

        }
    }
}
