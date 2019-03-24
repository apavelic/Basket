using Basket.Core.Models;
using System.Collections.Generic;

namespace Basket.Core.Interfaces
{
    public interface IProductService
    {
        List<ProductDTO> GetProducts();
    }
}
