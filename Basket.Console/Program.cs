using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Basket.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper helper = new ConsoleHelper();
            helper.ShowMainView();
        }
    }
}
