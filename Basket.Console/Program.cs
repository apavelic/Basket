using Basket.Core.Infrastructure;
using System;

namespace Basket.Console
{
    // Not sure if this layer is needed because in the assignment writes that no user interface is required. After, writes that I have to use logging to console 
    // with all details from basket, price, discounts etc. every time when total price is requested. That confused me and this is why I added this layer.

    // When this layer would not be needed, then Data layer would not be needed too (because I used data from database to be able to add new product to cart), 
    // and then Core (basket) would be standalone component without user interface.

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleHelper helper = new ConsoleHelper();
                helper.ShowMainView();
            }
            catch (Exception e)
            {
                Logger.Log($"Something went wrong.\n{e.Message}", LogType.Error);
            }
        }
    }
}
