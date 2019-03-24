using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Basket.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

// This service is there only becasue of adding a new products in basket which requires a data from database. This Core layer should be a standalone component
// and this product service have to be implemented in other (new) layer. But for simplify, I put that service here. Second options was to implement that within Console
// layer, but in that case SOLID principles wouldn't be fulfilled

namespace Basket.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public List<ProductDTO> GetProducts()
        {
            try
            {
                List<ProductDTO> model = new List<ProductDTO>();

                var products = _productRepository.GetAll().AsEnumerable();

                foreach (var product in products)
                {
                    model.Add(new ProductDTO
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Price = product.Price
                    });
                }

                return model;
            }
            catch (Exception e)
            {
                Logger.Log($"ERROR\nClass: ProductService.cs, Method: GetProducts, Message: {e.Message}", LogType.Error);
                throw e;
            }
        }
    }
}
