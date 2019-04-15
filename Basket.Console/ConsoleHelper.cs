using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Basket.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Console
{
    internal class ConsoleHelper
    {
        private readonly IBasket _basket;
        private readonly IProductService _productService;
        private readonly ILogger _logger;
        private readonly bool _hasDatabaseConnection;

        public ConsoleHelper()
        {
            // add dependecy injection
            _hasDatabaseConnection = RunApplication();

            _productService = new ProductService();
            _basket = new Core.Basket
            {
                HasDiscount = true
            };

            _basket.OnTotalPriceRequested += Basket_OnTotalPriceRequested;
            _logger = new Logger();

            if (_hasDatabaseConnection == false)
                InitializeCart();
        }
        public void ShowMainView()
        {


            System.Console.WriteLine("1. Add product to cart");
            System.Console.WriteLine("2. Request total price");
            System.Console.WriteLine("3. Empty cart");
            System.Console.WriteLine("4. Quit");
            int choice = -1;

            while (choice <= 0)
            {
                System.Console.Write("Please select a option (1/2/3/4): ");
                int.TryParse(System.Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        AddProductsView();
                        break;
                    case 2:
                        _basket.GetTotalPrice();
                        break;
                    case 3:
                        EmptyCart();
                        break;
                    case 4:
                        break;
                    default:
                        _logger.Log("Unsupported option. Please try again.", LogType.Warning);
                        choice = -1;
                        break;
                }
            }
        }
        private void EmptyCart()
        {
            System.Console.Clear();

            if (_basket.EmptyCart())
                _logger.Log("Chart is empty", LogType.Success);

            System.Console.WriteLine();
            ShowMainView();
        }
        private void InitializeCart()
        {
            _basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("25546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Bread",
                Price = 1,
                Quantity = 1
            });

            _basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("26546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Butter",
                Price = 0.8m,
                Quantity = 2
            });

            _basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("27546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Milk",
                Price = 1.15m,
                Quantity = 8
            });
        }
        private void AddProductsView()
        {
            System.Console.Clear();

            if (_hasDatabaseConnection == false)
            {
                _logger.Log("Access denied. The database was not initialized.", LogType.Warning);
                ShowMainView();
                return;
            }

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
                        System.Console.Write("Enter a product number to add products to cart: ");

                        int.TryParse(System.Console.ReadLine(), out choice);

                        try
                        {
                            var productToAdd = products[--choice];
                            bool quantityEntered = false;

                            while (quantityEntered == false)
                            {
                                try
                                {
                                    System.Console.Write("Quantity: ");
                                    productToAdd.Quantity = int.Parse(System.Console.ReadLine());

                                    if (productToAdd.Quantity == 0)
                                        throw new Exception();

                                    quantityEntered = true;

                                }
                                catch (Exception)
                                {
                                    _logger.Log("The Quantity must be a natural number. Please try again.", LogType.Warning);
                                }
                            }

                            if (_basket.Add(productToAdd))
                            {
                                System.Console.Clear();
                                _logger.Log("Product added successfully.\n", LogType.Success);
                                ShowMainView();
                            }
                        }
                        catch (Exception)
                        {
                            _logger.Log("No product with this product number. Please try again.", LogType.Warning);
                            choice = -1;
                        }
                    }
                }
                else
                {
                    _logger.Log("No data in database.", LogType.Warning);
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message, LogType.Warning);
            }

        }
        private void ShowBasketDetails(IEnumerable<ProductDTO> cart)
        {
            if (cart.Any())
            {
                System.Console.WriteLine("Product\t\tQuantity\tPrice\t\tTotal\t\tDiscount\tTotal with Discount");

                foreach (var item in cart)
                {
                    System.Console.WriteLine(item.ToString());
                } 
            }
            else
            {
                System.Console.WriteLine("Cart is empty.");
            }

            System.Console.WriteLine();

            ShowMainView();
        }
        private bool RunApplication()
        {
            try
            {
                System.Console.WriteLine("App started");

                var db = new InitDatabase();
                System.Console.WriteLine("Database initialization...");
                db.Init();

                System.Console.Clear();
                return true;

            }
            catch (Exception)
            {
                System.Console.Clear();
                _logger.Log("Can not connect to a database.\nPlease check your connection string. The app will start without using data layer and adding new products will be disabled.", LogType.Warning);
                return false;
            }
        }
        private void Basket_OnTotalPriceRequested(object sender, Core.Events.PriceEventArgs args)
        {
            System.Console.Clear();
            IBasket basket = (IBasket)sender;

            _logger.Log($"LOG [{args.Date}]: Total price requested: ${args.Price} (discount included)", LogType.Information);
            var cart = basket.GetCartContent();

            ShowBasketDetails(cart);
        }

    }
}
