using System;
using System.Collections.Generic;
using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.BaseService;

namespace FA.JustBlog.DataAccessLayer.Service.InterfaceService
{
    public interface IPostService : IBaseService<Post>
    {
        IList<Post> GetPublishPostedPosts();

        IList<Post> GetUnPublishPostedPosts();

        IList<Post> GetLatestPost(int size);

        IList<Post> GetPostByMonth(DateTime monthYear);

        int CountPostsForCategory(string category);

        IList<Post> GetPostsForCategory(string category);

        int CountPostsForTag(string tag);

        IList<Post> GetPostsForTag(string tag);

        IList<Post> GetMostViewdPost(int size);

        IList<Post> GetHighestPosts(int size);

        bool ChangePublisher(string id);

    }
}