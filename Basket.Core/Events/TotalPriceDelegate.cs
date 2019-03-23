using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Events
{
    public delegate void TotalPriceDelegate(object sender, PriceEventArgs args);
}
