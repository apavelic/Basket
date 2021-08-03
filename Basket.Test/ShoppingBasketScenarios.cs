using Basket.Core.Interfaces;
using Basket.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// Improve tests
namespace Basket.Test
{
	[TestClass]
	public class ShoppingBasketScenarios
	{
		[TestMethod]
		public void Scenario_DiscountOneOfEachTest()
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
		public void Scenario_DiscountTwoButtersTwoBreadsTest()
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
		public void Scenario_DiscountFourMilksTest()
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
		public void Scenario_DiscountEightMilksOneBreadTwoButtersTest()
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
		public void Scenario_DiscountEightMilks()
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
