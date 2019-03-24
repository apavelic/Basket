using Basket.Core.Infrastructure;
using System;

namespace Basket.Console
{
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
