using System;
using FA.JustBlog.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace FA.JustBlog.Core.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public void Dispose()
        {
            Context.Dispose();
        }

        protected ApplicationDbContext Context { get; set; }
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public int Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return Context.SaveChanges();
        }

        public bool Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Context.SaveChanges() > 0;
        }

        public virtual bool DeleteById(string id)
        {
            TEntity entity = Find(id);
            return Delete(entity);
        }

        public TEntity Find(string id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public bool Update(TEntity entity)
        {
            _dbSet.AddOrUpdate(entity);
            return Context.SaveChanges() > 0;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var item in includeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item); // Eager loading
            }
            return orderBy != null ? orderBy(query) : query;
        }


    }
}
