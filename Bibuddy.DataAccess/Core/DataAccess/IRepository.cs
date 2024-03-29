﻿using BiBuddy.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BiBuddy.DataAccess.Core.DataAccess
{
    //add limitation =>
    //Here class restriction used to reach from only reference type
    //new() => abstract and interface are a reference type ,so we are seperated from that.
    //IEntity => each classes can be able to have some specific for standard. 
    public interface IRepository<TEntity>
        where TEntity : class,IEntity,new()
    {
        void Add(TEntity entity);
        void Delete(int ID);
        void Update(TEntity entity);
        TEntity GetByID(int ID);
        List<TEntity> GetAll(string filter = null);
        int Count();
    }
}
