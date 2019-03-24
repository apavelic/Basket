using System.Collections.Generic;

namespace Basket.Core.Models.Discounts
{
    public class NoDiscount : ProductDiscount
    {
        public NoDiscount(List<ProductDTO> products) : 
            base(products)
        {

        }
    }
}
