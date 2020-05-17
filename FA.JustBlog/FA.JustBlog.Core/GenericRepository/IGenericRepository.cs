using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FA.JustBlog.Core.GenericRepository
{
    public interface IGenericRepository<TEntity> : IDisposable
    {
        TEntity Find(string id);

        int Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        bool DeleteById(string id);

        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter">Get follow condition</param>
        /// <param name="orderBy">Order by follow condition</param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}
