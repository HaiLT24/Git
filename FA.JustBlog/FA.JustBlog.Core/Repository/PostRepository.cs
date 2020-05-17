using FA.JustBlog.Core.GenericRepository;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.Core.Repository
{
    public class PostRepository : GenericRepository<Post>,IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public int CountPostsForCategory(string category)
        {
            return Context.Posts.Count(p => p.Category.Name.Contains(category));
        }

        public int CountPostsForTag(string tag)
        {
            return Context.Posts.Count(p => p.Tags.Count(t => t.Name.Contains(tag)) > 0);
        }

        public IList<Post> GetHighestPosts(int size)
        {
            return Context.Posts.OrderByDescending(p => (p.TotalRate / p.RateCount)).Take(size).ToList();
        }

        public IList<Post> GetLatestPost(int size)
        {
            return Context.Posts.OrderByDescending(p => p.Publisher).Take(size).ToList();
        }

        public IList<Post> GetMostViewdPost(int size)
        {
            return Context.Posts.OrderByDescending(p => p.ViewCount).Take(size).ToList();
        }

        public IList<Post> GetPostByMonth(DateTime monthYear)
        {
            return Context.Posts.Where(p => p.PostedOn.Month.Equals(monthYear.Month) && p.PostedOn.Year.Equals(monthYear.Year)).ToList();
        }

        public IList<Post> GetPostsForCategory(string category)
        {
            return Context.Posts.Where(p => p.Category.Name.Contains(category)).ToList();
        }

        public IList<Post> GetPostsForTag(string tag)
        {
            return Context.Posts.Where(p => p.Tags.Count(t => t.Name.Contains(tag)) > 0).ToList();
        }

        public IList<Post> GetPublishPostedPosts()
        {
            return Context.Posts.Where(p => p.Publisher).ToList();
        }

        public IList<Post> GetUnPublishPostedPosts()
        {
            return Context.Posts.Where(p => !p.Publisher).ToList();
        }

        public bool ChangePublisher(string id)
        {
            var post = Find(id);
            post.Publisher = !post.Publisher;
            Context.SaveChanges();

            return post.Publisher;
        }

    }
}
