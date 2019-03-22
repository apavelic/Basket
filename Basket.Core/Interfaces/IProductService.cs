using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetProducts();
    }
}
