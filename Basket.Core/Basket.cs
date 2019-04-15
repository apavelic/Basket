using Basket.Core.Events;
using Basket.Core.Factories;
using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Core
{
    public class Basket : IBasket
    {
        public event TotalPriceDelegate OnTotalPriceRequested;
        public bool HasDiscount { get; set; }
        private readonly ILogger _logger;

        private List<ProductDTO> shoppingCart;

        public Basket()
        {
            // TODO add dependecy injection
            // TODO remove this kind of logging and implement some external module fot that (with dependecy injection)
            _logger = new Logger();
            shoppingCart = new List<ProductDTO>();
        }
        public bool Add(ProductDTO productToAdd)
        {
            try
            {
                var product = shoppingCart.FirstOrDefault(x => x.ProductId == productToAdd.ProductId);

                if (product == null)
                    shoppingCart.Add(productToAdd);
                else
                    product.Quantity += productToAdd.Quantity;

                if (HasDiscount)
                    ApplyDiscount();

                return true;
            }
            catch (Exception e)
            {
                _logger.Log($"ERROR\nClass: Basket.cs, Method: Add, Message: {e.Message}", LogType.Error);
                return false;
            }
        }
        public decimal GetTotalPrice()
        {
            try
            {
                var price = shoppingCart.Sum(x => x.TotalPrice - x.Discount);

                if (OnTotalPriceRequested != null)
                {
                    OnTotalPriceRequested(this,
                        new PriceEventArgs() { ShoppingCart = shoppingCart, Price = price, Date = DateTime.Now }
                    );
                }

                return price;
            }
            catch (Exception e)
            {
                _logger.Log($"ERROR\nClass: Basket.cs, Method: GetTotalPrice, Message: {e.Message}", LogType.Error);
                throw e;
            }
        }
        public IEnumerable<ProductDTO> GetCartContent()
        {
            return shoppingCart;
        }
        public void ApplyDiscount()
        {
            try
            {
                List<DiscountType> possibleDiscounts = GetPossibleDiscounts();

                foreach (var discountType in possibleDiscounts)
                {
                    IDiscount model = DiscountFactory.GetDiscountModel(discountType, shoppingCart);
                    model.ApplyDiscount();
                }
            }
            catch (Exception e)
            {
                _logger.Log($"ERROR\nClass: Basket.cs, Method: ApplyDiscount, Message: {e.Message}", LogType.Error);
                throw e;
            }
        }
        public bool EmptyCart()
        {
            try
            {
                shoppingCart.Clear();
                return true;
            }
            catch (Exception e)
            {
                _logger.Log($"ERROR\nClass: Basket.cs, Method: EmptyCart, Message: {e.Message}", LogType.Error);
                return false;
            }
        }

        // TODO add smarter way to handle discounts
        private List<DiscountType> GetPossibleDiscounts()
        {
            try
            {
                List<DiscountType> discountTypes = new List<DiscountType>();

                foreach (var product in shoppingCart)
                {
                    ProductEnum type;
                    Enum.TryParse(product.Name, out type);

                    switch (type)
                    {
                        case ProductEnum.Milk:
                            if (product.Quantity > 3)
                                discountTypes.Add(DiscountType.MilkDiscount);
                            continue;

                        case ProductEnum.Butter:

                            if (product.Quantity > 1)
                                discountTypes.Add(DiscountType.BreadDiscount);
                            continue;
                    }
                }

                return discountTypes;
            }
            catch (Exception e)
            {
                _logger.Log($"ERROR\nClass: Basket.cs, Method: GetPossibleDiscounts, Message: {e.Message}", LogType.Error);
                throw e;
            }
        }
    }
}
