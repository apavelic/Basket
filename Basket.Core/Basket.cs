using Basket.Core.Events;
using Basket.Core.Factories;
using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Basket.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core
{
    public class Basket : IBasket
    {
        private List<ProductDTO> shoppingCart;
        public event TotalPriceDelegate OnTotalPriceRequested;

        public bool HasDiscount { get; set; }

        public Basket()
        {
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
                Logger.Log($"ERROR\nClass: Basket.cs, Method: Add, Message: {e.Message}", LogType.Error);
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
                Logger.Log($"ERROR\nClass: Basket.cs, Method: GetTotalPrice, Message: {e.Message}", LogType.Error);
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
                Logger.Log($"ERROR\nClass: Basket.cs, Method: ApplyDiscount, Message: {e.Message}", LogType.Error);
                throw e;
            }
        }

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
                Logger.Log($"ERROR\nClass: Basket.cs, Method: GetPossibleDiscounts, Message: {e.Message}", LogType.Error);
                throw e;
            }
        }
    }
}
