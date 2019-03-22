using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Core.Interfaces;

namespace Basket.Core.Models.Discounts
{
    public class MilkDiscount : ProductDiscount
    {
        int? threshold;

        public MilkDiscount(List<ProductDTO> products, int? threshold = 4) : 
            base(products)
        {
            this.threshold = threshold == 0 ? null : threshold;
        }

        public override void ApplyDiscount()
        {
            var filteredProduct = Products.FirstOrDefault(x => x.Name == Infrastructure.ProductEnum.Milk.ToString());

            if (threshold != null)
            {
                int numberOfFreeProducts = filteredProduct.Quantity / threshold.Value;
                filteredProduct.Discount = filteredProduct.Price * numberOfFreeProducts;
            }
        }
    }
}
