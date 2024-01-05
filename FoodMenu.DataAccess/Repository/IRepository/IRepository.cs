using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FoodMenu.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params string[] includeProperties);
        T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, params string[] includeProperties);
    }
}
