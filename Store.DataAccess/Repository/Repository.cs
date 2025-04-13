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

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = dbSet.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties)) // include properties exists
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
            
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null,string? includeProperties = null)  // inlude properties will be comma separated string
        {
            IQueryable<T> query = dbSet;
            if(filter!=null)
            query = dbSet.Where(filter);

            if(!string.IsNullOrEmpty(includeProperties)) // include properties exists
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
            //return dbSet.ToList();
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
