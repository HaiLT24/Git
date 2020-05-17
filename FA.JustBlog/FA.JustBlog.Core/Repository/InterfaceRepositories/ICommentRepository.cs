using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using FA.JustBlog.Core.GenericRepository;
using FA.JustBlog.Core.Models;


namespace FA.JustBlog.Core.Repository.InterfaceRepositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IList<Comment> GetCommentByPost(string id);
    }
}
