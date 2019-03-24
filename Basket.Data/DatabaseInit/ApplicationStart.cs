using System.Data.Entity;

namespace Basket.Data.DatabaseInit
{
    public class ApplicationStart
    {
        public void InitializeDatabase()
        {
            Database.SetInitializer<ApplicationContext>(new DatabaseInitialization());
            var context = new ApplicationContext();
            context.Database.Initialize(true);
        }
    }
}
