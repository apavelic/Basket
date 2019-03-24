using Basket.Data.DatabaseInit;

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
