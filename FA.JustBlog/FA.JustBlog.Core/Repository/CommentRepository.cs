using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using FA.JustBlog.Core.GenericRepository;

namespace FA.JustBlog.Core.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {

        }
        public IList<Comment> GetCommentByPost(string id)
        {
            return Context.Comments.Where(c => c.PostId.Equals(id)).ToList();
        }
    }
}
