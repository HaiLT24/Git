using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using FA.JustBlog.DataAccessLayer.BaseService;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;
using System;
using System.Collections.Generic;

namespace FA.JustBlog.DataAccessLayer.Service
{
    public class PostService : BaseService<Post>, IPostService
    {
        public IPostRepository PostRepository { get; set; }

        public PostService(IPostRepository postRepository) : base(postRepository)
        {
            PostRepository = postRepository;
        }

        public IList<Post> GetPublishPostedPosts()
        {
            return PostRepository.GetPublishPostedPosts();
        }

        public IList<Post> GetUnPublishPostedPosts()
        {
            return PostRepository.GetUnPublishPostedPosts();
        }

        public IList<Post> GetLatestPost(int size)
        {
            if (size < 0)
            {
                size = 0;
            }

            return PostRepository.GetLatestPost(size);
        }

        public IList<Post> GetPostByMonth(DateTime monthYear)
        {
            return PostRepository.GetPostByMonth(monthYear);
        }

        public int CountPostsForCategory(string category)
        {
            if (category is null)
            {
                throw new NullReferenceException();
            }

            return PostRepository.CountPostsForCategory(category);
        }

        public IList<Post> GetPostsForCategory(string category)
        {
            if (category is null)
            {
                throw new NullReferenceException();
            }

            return PostRepository.GetPostsForCategory(category);
        }

        public int CountPostsForTag(string tag)
        {
            if (tag is null)
            {
                throw new NullReferenceException();
            }

            return PostRepository.CountPostsForTag(tag);
        }

        public IList<Post> GetPostsForTag(string tag)
        {
            if (tag is null)
            {
                throw  new NullReferenceException();
            }

            return PostRepository.GetPostsForTag(tag);
        }

        public IList<Post> GetMostViewdPost(int size)
        {
            return PostRepository.GetMostViewdPost(size);
        }

        public IList<Post> GetHighestPosts(int size)
        {
            return PostRepository.GetHighestPosts(size);
        }

        public bool ChangePublisher(string id)
        {
            return PostRepository.ChangePublisher(id);
        }
    }
}