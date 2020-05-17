using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FA.JustBlog.Core.GenericRepository;
using FA.JustBlog.DataAccessLayer.Common;

namespace FA.JustBlog.DataAccessLayer.BaseService
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        public void Dispose()
        {
            GenericRepository.Dispose();
        }

        protected IGenericRepository<TEntity> GenericRepository { get; set; }

        public BaseService(IGenericRepository<TEntity> genericRepository)
        {
            GenericRepository = genericRepository;
        }

        public TEntity Find(string id)
        {
            return GenericRepository.Find(id);
        }

        public int Add(TEntity entity)
        {
            if (entity is null)
            {
                throw new NullReferenceException();
            }

            return GenericRepository.Add(entity);
        }

        public bool Update(TEntity entity)
        {
            if (entity is null)
            {
                throw new NullReferenceException();
            }

            return GenericRepository.Update(entity);
        }

        public bool Delete(TEntity entity)
        {
            if (entity is null)
            {
                throw new NullReferenceException();
            }

            return GenericRepository.Delete(entity);
        }

        public bool DeleteById(string id)
        {
            if (Find(id) is null)
            {
                throw new NullReferenceException();
            }

            return GenericRepository.DeleteById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GenericRepository.GetAll();
        }

        public async Task<PaginatedList<TEntity>> GetWithPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageIndex = 1,
            int pageSize = 10)
        {
            var query = GenericRepository.Get(filter, orderBy, includeProperties);

            return await PaginatedList<TEntity>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        
    }
}