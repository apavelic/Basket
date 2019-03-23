using Basket.Core.Events;
using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IBasket
    {
        IEnumerable<ProductDTO> GetCartContent();
        void Add(ProductDTO product);
        void ApplyDiscount();
        bool HasDiscount { get; set; }
        decimal TotalPriceWithDiscount { get; }
        decimal TotalPrice { get; }

        event TotalPriceDelegate OnTotalPriceRequested;
        event TotalPriceDelegate OnTotalPriceWithDiscountRequested;

    }
}
