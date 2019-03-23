using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Basket.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunApplication();

            IBasket basket = new Core.Basket
            {
                HasDiscount = true
            };

            basket.OnTotalPriceRequested += Basket_OnTotalPriceRequested;
            basket.OnTotalPriceWithDiscountRequested += Basket_OnTotalPriceWithDiscountRequested; ;

            InitializeProducts(basket);

            if (basket.HasDiscount)
            {
                basket.ApplyDiscount();
            }

            ShowBasketDetails(basket);
        }

        private static void Basket_OnTotalPriceWithDiscountRequested(object sender, Core.Events.PriceEventArgs args)
        {
            Logger.Log($"LOG [{args.Date}]: Totalll price with discount requested: {args.Price}");
        }

        private static void Basket_OnTotalPriceRequested(object sender, Core.Events.PriceEventArgs args)
        {
            Logger.Log($"LOG [{args.Date}]: Totalll price without discount requested: {args.Price}");
        }

        private static void RunApplication()
        {
            System.Console.Write("Type 'y' if you want to use databse: ");
            string answer = System.Console.ReadLine();
            System.Console.Clear();
            if (answer == "y")
            {
                System.Console.WriteLine("Application started");

                var db = new InitDatabase();
                System.Console.WriteLine("Database initialization...");
                db.Init();

                System.Console.Clear();
            }
        }

        private static void InitializeProducts(IBasket basket)
        {
            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("25546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Bread",
                Price = 1,
                Quantity = 1
            });

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("26546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Butter",
                Price = 0.8m,
                Quantity = 2
            });

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("27546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Milk",
                Price = 1.15m,
                Quantity = 8
            });
        }

        private static void ShowBasketDetails(IBasket basket)
        {
            System.Console.WriteLine("Product\t\tQuantity\tPrice\t\tTotal\t\tDiscount\tTotal with Discount");

            var products = basket.GetCartContent().ToList();
            foreach (var item in products)
            {
                System.Console.WriteLine(item.ToString());
            }
            System.Console.WriteLine("####################################################################################################");

            System.Console.WriteLine($"Total price: ${basket.TotalPrice}");
            System.Console.WriteLine($"Total price with discount: ${basket.TotalPriceWithDiscount}");
        }
    }
}
