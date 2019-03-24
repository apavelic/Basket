using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Basket.Core.Models.Discounts;
using System.Collections.Generic;

namespace Basket.Core.Factories
{
    public class DiscountFactory
    {
        public static IDiscount GetDiscountModel(DiscountType type, List<ProductDTO> products)
        {
            switch (type)
            {
                case DiscountType.MilkDiscount:
                    return new MilkDiscount(products);

                case DiscountType.BreadDiscount:
                    return new BreadDiscount(products);

                default:
                    return new NoDiscount(products);
            }
        }
    }
}
