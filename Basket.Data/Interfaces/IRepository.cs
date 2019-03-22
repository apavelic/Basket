using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        List<T> GetAll();
        T FindBy(Expression<Func<T, bool>> expression);
    }
}
