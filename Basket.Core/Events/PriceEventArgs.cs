﻿using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Events
{
    public class PriceEventArgs : EventArgs
    {
        public List<ProductDTO> ShoppingCart { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}