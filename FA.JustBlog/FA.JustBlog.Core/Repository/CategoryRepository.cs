using System;
using System.Data.Entity;
using System.Linq;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using FA.JustBlog.Core.GenericRepository;

namespace FA.JustBlog.Core.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override bool DeleteById(string id)
        {
            using (var dbTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var listPostByCategory = Context.Set<Post>().Where(p => p.CategoryId == id);
                    

                    if (listPostByCategory.Any())
                    {
                        foreach (var post in listPostByCategory)
                        {
                            post.CategoryId = null;
                        }
                    }
                    base.DeleteById(id);

                    dbTransaction.Commit();

                }
                catch
                {
                    dbTransaction.Rollback();
                }
                return Context.SaveChanges() > 0;
            }
        }
    }
}
