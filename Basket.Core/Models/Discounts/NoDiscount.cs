using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Core.Interfaces;

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
