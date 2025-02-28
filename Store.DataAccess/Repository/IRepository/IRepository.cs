﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        //T Category

        //GetAll
        IEnumerable<T> GetAll();

        //Get
        T Get(Expression<Func<T,bool>>filter);
        //Add
        void Add(T entity);
        //Remove
        void Remove(T entity);
        //RemoveRange
        void RemoveRange(IEnumerable<T> entities);
    }
}
