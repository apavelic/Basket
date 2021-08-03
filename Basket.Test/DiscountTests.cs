using Basket.Core.Interfaces;
using Basket.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// Improve tests
namespace Basket.Test
{
	[TestClass]
	public class DiscountTests
	{
		[TestMethod]
		public void GetTotalPriceOneOfEachTest()
		{
			// arrange, act
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

			// assert
			Assert.AreEqual(2.95m, basket.GetTotalPrice());
		}



		[TestMethod]
		public void GetTotalPriceTwoButtersTwoBreadsTest()
		{
			// arrange, act
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

			// assert
			Assert.AreEqual(3.10m, basket.GetTotalPrice());
		}


		[TestMethod]
		public void GetTotalPriceFourMilksTest()
		{
			// arrange, act
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

			// assert
			Assert.AreEqual(3.45m, basket.GetTotalPrice());
		}

		[TestMethod]
		public void GetTotalPriceEightMilksOneBreadTwoButtersTest()
		{
			// arrange, act
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

			// assert
			Assert.AreEqual(9.00m, basket.GetTotalPrice());
		}

		[TestMethod]
		public void GetTotalPriceEightMilks()
		{
			// arrange, act
			IBasket basket = new Core.Basket
			{
				HasDiscount = true
			};

			basket.Add(new ProductDTO()
			{
				ProductId = Guid.Parse("27546792-9A4C-E911-93A5-144F8A014C66"),
				Name = "Milk",
				Price = 1.15m,
				Quantity = 8
			});

			if (basket.HasDiscount)
				basket.ApplyDiscount();

			// assert
			Assert.AreEqual(6.90m, basket.GetTotalPrice());
		}
	}
}
