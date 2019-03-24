using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Services;

namespace Basket.Core.Models.Discounts
{
    public class BreadDiscount : ProductDiscount
    {

        public BreadDiscount(List<ProductDTO> products) :
            base(products)
        {

        }

        public override void ApplyDiscount()
        {
            var butter = Products.FirstOrDefault(x => x.Name == ProductEnum.Butter.ToString());

            if (butter != null)
            {
                var bread = Products.FirstOrDefault(x => x.Name == ProductEnum.Bread.ToString());

                if (bread != null)
                {
                    int numberOfBreadsToDiscount = butter.Quantity / 2;

                    if (bread.Quantity < numberOfBreadsToDiscount)
                        numberOfBreadsToDiscount = bread.Quantity;

                    bread.Discount = numberOfBreadsToDiscount * bread.Price / 2;
                }
            }
        }
    }
}
