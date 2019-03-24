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
        bool Add(ProductDTO product);
        void ApplyDiscount();
        bool HasDiscount { get; set; }
        decimal GetTotalPrice();

        event TotalPriceDelegate OnTotalPriceRequested;

    }
}
