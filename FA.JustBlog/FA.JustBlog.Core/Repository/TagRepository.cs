using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using System.Collections.Generic;
using System.Linq;
using FA.JustBlog.Core.GenericRepository;


namespace FA.JustBlog.Core.Repository
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Tag GetTagByUrlSlug(string urlSlug)
        {
            return Context.Tags.FirstOrDefault(t => t.UrlSlug.Contains(urlSlug));
        }

        public IEnumerable<Tag> GetTagForCount(int size)
        {
            return Context.Tags.OrderByDescending(c => c.Count).Take(size);
        }

        public void CalculateCountTags()
        {
            foreach (Tag tag in Context.Tags.Include("Posts").ToList())
            {
                tag.Count = (tag.Posts is null ? 0 : tag.Posts.Count());
            }

            Context.SaveChanges();
        }


    }
}
