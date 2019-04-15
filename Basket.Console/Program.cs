using Basket.Core.Infrastructure;
using System;

namespace Basket.Console
{
    // Console application for testing

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper helper = new ConsoleHelper();
            helper.ShowMainView();
        }
    }
}
