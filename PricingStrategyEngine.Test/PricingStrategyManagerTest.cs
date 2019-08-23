using System;
using Application.PricingStrategyManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PricingStrategyEngine.Test
{
    [TestClass]
    public class PricingStrategyManagerTest
    {

        [TestMethod]
        public void GetPricingStrategy_GetCalled_ShouldReturnIPrcingStrategyType()
        {
            //Arrange
            var manager = new PricingStrategyManager();
            char supply = 'H', demand = 'H';

            //Act
            var obj = manager.GetPricingStrategy(supply, demand);

            //Assert
            Assert.IsInstanceOfType(obj, obj.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Supply or Demand value is not correct.")]
        public void GetPricingStrategy_WhenSupplyRandomAndDemandLow_ShouldReturn5PerLess()
        {
            //Arrange
            double itemPrice = 10.0;
            var manager = new PricingStrategyManager();
            char supply = 'R', demand = 'L';
            double expectedResult = 11;

            //Act
            var obj = manager.GetPricingStrategy(supply, demand);
        }

        [TestMethod]
        public void GetPricingStrategy_WhenSupplyHighAndDemandHigh_ShouldReturnSameValue()
        {
            //Arrange
            double itemPrice = 10.0;
            var manager = new PricingStrategyManager();
            char supply = 'H', demand = 'H';
            double expectedResult = 10.0;
            
            //Act
            var obj= manager.GetPricingStrategy(supply, demand);
            double actualResult = obj.GetPrice(itemPrice);

            //Assert
            Assert.AreEqual(expectedResult, actualResult, "On High Suppy and High demand value should be same as price.");
        }

        [TestMethod]
        public void GetPricingStrategy_WhenSupplyHighAndDemandLow_ShouldReturn5PerLess()
        {
            //Arrange
            double itemPrice = 10.0;
            var manager = new PricingStrategyManager();
            char supply = 'H', demand = 'L';
            double expectedResult = 9.5;

            //Act
            var obj = manager.GetPricingStrategy(supply, demand);
            double actualResult = obj.GetPrice(itemPrice);

            //Assert
            Assert.AreEqual(expectedResult, actualResult, "On High Supply and High demand value should be same as price.");
        }
        [TestMethod]
        public void GetPricingStrategy_WhenSupplyLowAndDemandHigh_ShouldReturn5PerMore()
        {
            //Arrange
            double itemPrice = 10.0;
            var manager = new PricingStrategyManager();
            char supply = 'L', demand = 'H';
            double expectedResult = 10.5;
            
            //Act
            var obj = manager.GetPricingStrategy(supply, demand);
            double actualResult = obj.GetPrice(itemPrice);

            //Assert
            Assert.AreEqual(expectedResult, actualResult, "On High Supply and High demand value should be same as price.");
        }

        [TestMethod]
        public void GetPricingStrategy_WhenSupplyLowAndDemandLow_ShouldReturn10PerMore()
        {
            //Arrange
            double itemPrice = 10.0;
            var manager = new PricingStrategyManager();
            char supply = 'L', demand = 'L';
            double expectedResult = 11;

            //Act
            var obj = manager.GetPricingStrategy(supply, demand);
            double actualResult = obj.GetPrice(itemPrice);

            //Assert
            Assert.AreEqual(expectedResult, actualResult, "On High Supply and High demand value should be same as price.");
        }

        
    }
}
