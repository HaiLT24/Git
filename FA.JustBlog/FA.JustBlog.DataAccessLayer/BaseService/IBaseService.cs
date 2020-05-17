using FA.JustBlog.DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FA.JustBlog.DataAccessLayer.BaseService
{
    public interface IBaseService<TEntity> :IDisposable where TEntity : class
    {
        TEntity Find(string id);

        int Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        bool DeleteById(string id);

        IEnumerable<TEntity> GetAll();

        Task<PaginatedList<TEntity>> GetWithPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, 
                IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageIndex = 1,
            int pageSize = 10);
    }
}
