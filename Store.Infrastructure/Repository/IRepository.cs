using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Store.Infrastructure.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
    }
}
