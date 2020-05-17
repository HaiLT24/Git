using System.Collections;
using System.Collections.Generic;
using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.BaseService;

namespace FA.JustBlog.DataAccessLayer.Service.InterfaceService
{
    public interface ICommentService : IBaseService<Comment>
    {
        IList<Comment> GetCommentByPost(string id);
    }
}