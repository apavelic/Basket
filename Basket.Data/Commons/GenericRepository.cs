using Basket.Data.DatabaseInit;
using Basket.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Data.Commons
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private bool disposed = false;

        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        internal GenericRepository()
        {
            _context = new ApplicationContext();
            _dbSet = _context.Set<T>();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
