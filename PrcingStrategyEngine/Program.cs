using Application.PricingStrategyManager;
using Domain.Model;

namespace PrcingStrategyEngine
{
    using System;
    using System.Collections.Generic;

    //When High Supply and High Demand then Product will be sold at same price
    //When Low Supply and Low Demand Then Product will be sold 10% more
    //When Low Supply and High Demand Then Product will be sold 5% more
    //When High Supply and Low Demand Then Product will be sold 5% less

    public class Program
    {
        public static void Main()
        {

            var strategyManager = new PricingStrategyManager();
            //Enter # of Products for which price to be find
            int noOfProducts = Convert.ToInt32(Console.ReadLine());
            
            //Add Product,  Supply and Demand
            List<Item> itemList = new List<Item>();
            for (int i = 0; i < noOfProducts; i++)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrEmpty(command))
                    throw new ArgumentNullException("command", "Command is not valid");

                string[] commandSplit = command.Split(' ');

                if (commandSplit.Length == 3)
                {
                    char supply = commandSplit[1][0];
                    char demand = commandSplit[2][0];
                    var pricingStrategy = strategyManager.GetPricingStrategy(supply,demand);
                    var item = new Item(pricingStrategy) {Name = commandSplit[0]};
                    itemList.Add(item);
                }
            }

            //int noOfSurveys = Convert.ToInt32(Console.ReadLine());

            itemList.ForEach(x => Console.WriteLine(x.Name));
            Console.ReadLine();
        }


        
       

    }
}
