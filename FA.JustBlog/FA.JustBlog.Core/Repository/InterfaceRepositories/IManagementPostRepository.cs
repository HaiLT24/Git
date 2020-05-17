using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repository.InterfaceRepositories
{
    public interface IManagementPostRepository
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
