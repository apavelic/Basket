using Basket.Core.Infrastructure;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Basket.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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
