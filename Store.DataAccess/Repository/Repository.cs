using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository
{
    public class Repository<T>:IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            dbSet = _db.Set<T>();

            /*
             Example:
            _db.Categories =dbSet
            _db.Categories.Add() = dbset.Add()
             */
        }
        public void Add(T entity)
        {

            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            //IQueryable<T> query = dbSet;
            //query = dbSet.Where(filter);
            //return query.FirstOrDefault();
            return dbSet.FirstOrDefault(filter);
        }

        public IEnumerable<T> GetAll()
        {
            //IQueryable<T> query = dbSet;
            //return query.ToList();
            return dbSet.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
