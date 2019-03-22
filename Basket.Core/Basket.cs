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

        public bool HasDiscount { get; set; }
        public decimal TotalPrice
        {
            get
            {
                var price = shoppingCart.Sum(x => x.TotalPrice);
                Logger.Log($"LOG: Total price without discount requested: {price}");
                return price;
            }
        }
        public decimal TotalPriceWithDiscount
        {
            get
            {
                var price = shoppingCart.Sum(x => x.TotalPrice - x.Discount);
                Logger.Log($"LOG: Total price with discount requested: {price}");
                return price;
            }
        }

        public Basket()
        {
            shoppingCart = new List<ProductDTO>();
        }

        public void Add(ProductDTO productToAdd)
        {
            var product = shoppingCart.FirstOrDefault(x => x.ProductId == productToAdd.ProductId);

            if (product == null)
                shoppingCart.Add(productToAdd);
            else
                product.Quantity += productToAdd.Quantity;
        }

        public IEnumerable<ProductDTO> GetCartContent()
        {
            return shoppingCart;
        }

        public void ApplyDiscount()
        {
            List<DiscountType> possibleDiscounts = GetPossibleDiscounts();

            foreach (var discountType in possibleDiscounts)
            {
                IDiscount model = DiscountFactory.GetDiscountModel(discountType, shoppingCart);
                model.ApplyDiscount();
            }
        }

        private List<DiscountType> GetPossibleDiscounts()
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
    }
}
