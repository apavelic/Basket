using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Basket.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        List<T> GetAll();
        T FindBy(Expression<Func<T, bool>> expression);
    }
}
