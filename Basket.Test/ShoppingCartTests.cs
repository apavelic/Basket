using System;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Basket.Test
{
    [TestClass]
    public class ShoppingCartTests
    {
        [TestMethod]
        public void GetTotalPriceOneOfEachTest()
        {

            IBasket basket = new Core.Basket
            {
                HasDiscount = true
            };

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("25546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Bread",
                Price = 1,
                Quantity = 1
            });

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("26546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Butter",
                Price = 0.8m,
                Quantity = 1
            });

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("27546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Milk",
                Price = 1.15m,
                Quantity = 1
            });

            if (basket.HasDiscount)
                basket.ApplyDiscount();

            decimal expected = 2.95m;
            Assert.AreEqual(expected, basket.TotalPriceWithDiscount);
        }



        [TestMethod]
        public void GetTotalPriceTwoButtersTwoBreadsTest()
        {

            IBasket basket = new Core.Basket
            {
                HasDiscount = true
            };

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("26546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Butter",
                Price = 0.8m,
                Quantity = 2
            });

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("25546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Bread",
                Price = 1.00m,
                Quantity = 2
            });

            if (basket.HasDiscount)
                basket.ApplyDiscount();

            decimal expected = 3.10m;
            Assert.AreEqual(expected, basket.TotalPriceWithDiscount);
        }


        [TestMethod]
        public void GetTotalPriceFourMilksTest()
        {
            IBasket basket = new Core.Basket
            {
                HasDiscount = true
            };

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("27546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Milk",
                Price = 1.15m,
                Quantity = 4
            });

            if (basket.HasDiscount)
                basket.ApplyDiscount();

            decimal expected = 3.45m;
            Assert.AreEqual(expected, basket.TotalPriceWithDiscount);
        }

        [TestMethod]
        public void GetTotalPriceEightMilksOneBreadTwoButtersTest()
        {

            IBasket basket = new Core.Basket
            {
                HasDiscount = true
            };

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("25546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Bread",
                Price = 1,
                Quantity = 1
            });

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("26546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Butter",
                Price = 0.8m,
                Quantity = 2
            });

            basket.Add(new ProductDTO()
            {
                ProductId = Guid.Parse("27546792-9A4C-E911-93A5-144F8A014C66"),
                Name = "Milk",
                Price = 1.15m,
                Quantity = 8
            });

            if (basket.HasDiscount)
                basket.ApplyDiscount();

            decimal expected = 9.00m;
            Assert.AreEqual(expected, basket.TotalPriceWithDiscount);
        }
    }
}
