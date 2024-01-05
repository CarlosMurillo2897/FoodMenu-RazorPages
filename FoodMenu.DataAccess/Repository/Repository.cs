using FoodMenu.DataAccess.Data;
using FoodMenu.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FoodMenu.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal DbSet<T> DbSet;
        private readonly ApplicationDbContext _db;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params string[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, params string[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
