using Basket.Data.DatabaseInit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Infrastructure
{
    public class InitDatabase
    {
        public void Init()
        {
            ApplicationStart applicationStart = new ApplicationStart();
            applicationStart.InitializeDatabase();
        }
    }
}
