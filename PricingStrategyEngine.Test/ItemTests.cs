using System;
using System.Collections.Generic;
using System.Linq;
using Application.PricingStrategyManager;
using Domain.Interfaces;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PricingStrategyEngine.Test
{
    [TestClass]
    public class ItemTests
    {
        private IPricingStrategyManager manager;
        private char demand;
        private char supply;
        private Item item;

        [TestInitialize]
        public void OnInitialize()
        {
            manager = new PricingStrategyManager();
            this.supply = 'H';
            this.demand = 'H';
            item = new Item(manager.GetPricingStrategy(supply, demand));
        }
        
        [TestMethod]
        public void AddSurvey_WhenOnlySingleSurvey_ShouldReturnSurveyCountAsOne()
        {
            //Arrange
            double itemPrice = 100.0;
            string productName = "ssd";
            string surveyName = "x";
            double expectedSurveyCount = 1;
            Item item = new Item(manager.GetPricingStrategy(supply, demand));
            //Act
            ItemSurvey itemsurvey = new ItemSurvey() { ItemName = productName, Price = itemPrice, SurveyName = surveyName };
            
            item.AddSurvey(itemsurvey);

            //Assert
            Assert.AreEqual(expectedSurveyCount, item.SurveyCount, "Should return single price for survey");

        }
        [TestMethod]
        public void AddSurvey_When2eValidSurvey_ShouldReturnSurveyCountAs2()
        {
            //Arrange
            double itemPrice = 100.0;
            string productName = "ssd";
            string surveyName = "x";
            double expectedSurveyCount = 2;

            //Act
            ItemSurvey itemsurvey1 = new ItemSurvey() { ItemName = productName, Price = itemPrice, SurveyName = surveyName };
            item.AddSurvey(itemsurvey1);
            ItemSurvey itemsurvey2 = new ItemSurvey() { ItemName = productName, Price = itemPrice + 1, SurveyName = surveyName };
            item.AddSurvey(itemsurvey2);
            //Assert
            Assert.AreEqual(expectedSurveyCount, item.SurveyCount, "Should return single price for survey");

        }
        [TestMethod]
        public void AddSurvey_AddingSurveyWithPriceLessThan50PerAverageSurveyPrice_ShouldReturnSurveyCountAsOne()
        {
            //Arrange
            double itemPrice = 100.0;
            string productName = "ssd";
            string surveyName = "x";
            double expectedSurveyCount = 1;
            Item item = new Item(manager.GetPricingStrategy(supply, demand));
            
            //Act
            ItemSurvey itemsurvey1 = new ItemSurvey() { ItemName = productName, Price = itemPrice, SurveyName = surveyName };
            item.AddSurvey(itemsurvey1);
            ItemSurvey itemsurvey2 = new ItemSurvey() { ItemName = productName, Price = (itemPrice/2-10), SurveyName = surveyName };
            item.AddSurvey(itemsurvey2);
            

            //Assert
            Assert.AreEqual(expectedSurveyCount, item.SurveyCount, "Should return single price for survey");

        }
        [TestMethod]
        public void AddSurvey_AddingMultipleSurveyWithPriceLessThan50PerAverageSurveyPrice_ShouldReturnSurveyCountAsOne()
        {
            //Arrange
            double itemPrice = 100.0;
            string productName = "ssd";
            string surveyName = "x";
            double expectedSurveyCount = 1;
            Item item = new Item(manager.GetPricingStrategy(supply, demand));

            //Act
            ItemSurvey itemsurvey1 = new ItemSurvey() { ItemName = productName, Price = itemPrice, SurveyName = surveyName };
            item.AddSurvey(itemsurvey1);
            ItemSurvey itemsurvey2 = new ItemSurvey() { ItemName = productName, Price = (itemPrice / 2 - 10), SurveyName = surveyName };
            item.AddSurvey(itemsurvey2);


            //Assert
            Assert.AreEqual(expectedSurveyCount, item.SurveyCount, "Should return single price for survey");

        }
        [TestMethod]
        public void ItemPrice_HighDemandAndHighSupplyWhenNoSurvey_ShouldReturnPriceAsNull()
        {
            //Arrange
            string productName = "ssd";
            string surveyName = "x";

            //Act
            item = new Item(manager.GetPricingStrategy(supply, demand));

            //Assert
            Assert.IsNull(item.Price, "Should return price as null for item.");

        }

        [TestMethod]
        public void ItemPrice_WhenOnlySingleSurvey_ShouldReturnSamePrice()
        {
            //Arrange
            double itemPrice = 100.0;
            string productName = "ssd";
            string surveyName = "x";
            double expected = 100.0;

            //Act
            ItemSurvey itemsurvey = new ItemSurvey() { ItemName = productName, Price = itemPrice, SurveyName = surveyName };
            item.AddSurvey(itemsurvey);

            //Assert
            Assert.AreEqual(expected, item.Price, "Should return single price for survey");

        }

        [TestMethod]
        public void ItemPrice_WhenOnlyMultipleSurvey_ShouldReturnLowestPrice()
        {
            //Arrange
            List<double> surveyList = new List<double>() { 129.0, 120.0, 121.0 };
            string productName = "ssd";

            double expected = 120.0;
            //Act
            surveyList.ForEach(x =>
            {
                var itemsurvey = new ItemSurvey() { ItemName = productName, Price = x, SurveyName = "xxx" };
                item.AddSurvey(itemsurvey);

            });

            //Assert
            Assert.AreEqual(expected, item.Price, "Should return lowest price from multiple surveys.");

        }
        [TestMethod]
        public void ItemPrice_WhenOnlyMultipleSurveyWithPriceLessThan50PerOfAveragePrice_ShouldNotConsiderThatPrice()
        {
            //Arrange
            List<double> surveyList = new List<double>() { 10.0, 11, 12, 1 };
            string productName = "ssd";

            double expectedSurveyCount = 3;
            //Act
            surveyList.ForEach(x =>
            {
                var itemsurvey = new ItemSurvey() { ItemName = productName, Price = x, SurveyName = "xxx" };
                item.AddSurvey(itemsurvey);

            });

            //Assert
            Assert.AreEqual(expectedSurveyCount, item.SurveyCount,"Should not consider survey whose price lower than 50% of avrage price");

        }
        [TestMethod]
        public void ItemPrice_WhenOnlyMultipleSurveyWithPriceMoreThan50PerOfAveragePrice_ShouldNotConsiderThatPrice()
        {
            //Arrange
            List<double> surveyList = new List<double>() { 10.0, 11, 12, 45 };
            string productName = "ssd";

            double expectedSurveyCount = 3;
            //Act
            surveyList.ForEach(x =>
            {
                var itemsurvey = new ItemSurvey() { ItemName = productName, Price = x, SurveyName = "xxx" };
                item.AddSurvey(itemsurvey);

            });

            //Assert
            Assert.AreEqual(expectedSurveyCount, item.SurveyCount, "Should not consider survey whose price lower than 50% of avrage price");

        }
       
    }
}
