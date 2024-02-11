using App.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace App.Service.Repository
{
   public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FristOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        IEnumerable<CategoryCnotent> AddCategoryCnotent(IEnumerable<CategoryCnotent> entities);
        void Update(TEntity entity, TEntity context);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
