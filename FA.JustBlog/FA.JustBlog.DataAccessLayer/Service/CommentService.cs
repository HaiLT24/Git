using System.Collections.Generic;
using FA.JustBlog.Core.GenericRepository;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using FA.JustBlog.DataAccessLayer.BaseService;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;

namespace FA.JustBlog.DataAccessLayer.Service
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        public ICommentRepository CommentRepository { get; set; }

        public CommentService(ICommentRepository commentRepository) : base(commentRepository)
        {
            CommentRepository = commentRepository;
        }

        public IList<Comment> GetCommentByPost(string id)
        {
            return CommentRepository.GetCommentByPost(id);
        }
    }
}