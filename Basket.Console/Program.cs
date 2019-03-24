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
        private static bool hasDatabaseConnection;

        static void Main(string[] args)
        {
            hasDatabaseConnection = RunApplication();

            IBasket basket = new Core.Basket
            {
                HasDiscount = true
            };

            basket.OnTotalPriceRequested += Basket_OnTotalPriceRequested;

            InitializeProducts(basket);
            ShowMainView(basket);
        }

        private static void ShowMainView(IBasket basket)
        {


            System.Console.WriteLine("1. Add product to cart");
            System.Console.WriteLine("2. Request total price");
            System.Console.WriteLine("3. Quit");
            int choice = -1;

            while (choice <= 0)
            {
                System.Console.Write("Please select (1/2/3): ");
                int.TryParse(System.Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        if (hasDatabaseConnection)
                        {
                            AddProductsView(basket);
                        }
                        else
                        {
                            System.Console.Clear();
                            Logger.Log("Access denied. The database was not initialized.", LogType.Warning);
                            ShowMainView(basket);
                        }
                        break;
                    case 2:
                        basket.GetTotalPrice();
                        break;
                    case 3:
                        break;
                    default:
                        Logger.Log("Unsupported coice.\nPlease try again and enter supported coice (1, 2 or 3).", LogType.Warning);
                        choice = -1;
                        break;
                }
            }
        }
        private static void AddProductsView(IBasket basket)
        {
            System.Console.Clear();

            IProductService productService = new ProductService();
            List<ProductDTO> products = null;

            try
            {
                products = productService.GetProducts();

                if (products.Any())
                {
                    System.Console.WriteLine("Product number\tProduct name\tProduct price");

                    for (int i = 0; i < products.Count; i++)
                    {
                        System.Console.WriteLine($"{i + 1}\t\t{products[i].Name} \t\t${products[i].Price}");
                    }

                    int choice = -1;

                    while (choice == -1)
                    {
                        System.Console.Write("Enter the product number to add product to cart: ");

                        int.TryParse(System.Console.ReadLine(), out choice);

                        try
                        {
                            var productToAdd = products[--choice];

                            while (true)
                            {
                                try
                                {
                                    System.Console.Write("Quantity: ");
                                    productToAdd.Quantity = int.Parse(System.Console.ReadLine());
                                    break;
                                }
                                catch (Exception)
                                {
                                    Logger.Log("The Quantity must be a natural number. Please try again.", LogType.Warning);
                                }
                            }

                            if (basket.Add(productToAdd))
                            {
                                System.Console.Clear();
                                Logger.Log("Product added successfully.\n", LogType.Success);
                                Thread.Sleep(1000);
                                ShowMainView(basket);
                            }
                        }
                        catch (Exception)
                        {
                            Logger.Log("No product with this product number. Please try again.", LogType.Warning);
                            choice = -1;
                        }
                    }
                }
                else
                {
                    Logger.Log("No data in database.", LogType.Warning);
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogType.Warning);
            }

        }

        private static void Basket_OnTotalPriceRequested(object sender, Core.Events.PriceEventArgs args)
        {
            System.Console.Clear();
            IBasket basket = (IBasket)sender;

            Logger.Log($"LOG [{args.Date}]: Total price requested: ${args.Price} (discount included)", LogType.Information);
            var cart = basket.GetCartContent();

            ShowBasketDetails(basket);
        }

        private static bool RunApplication()
        {
            try
            {
                System.Console.WriteLine("Application started");

                var db = new InitDatabase();
                System.Console.WriteLine("Database initialization...");
                db.Init();

                System.Console.Clear();
                return true;

            }
            catch (Exception ex)
            {
                System.Console.Clear();
                Logger.Log("Please check you connection string to initialize database. Application will start without using Data Access Layer and available products will not show if you try to add new product", LogType.Warning);
                return false;
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

            foreach (var item in basket.GetCartContent())
            {
                System.Console.WriteLine(item.ToString());
            }

            System.Console.WriteLine("\n");
            ShowMainView(basket);
        }
    }
}
