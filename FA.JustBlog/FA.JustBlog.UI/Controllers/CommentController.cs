using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;

namespace FA.JustBlog.UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;

        }
        // GET: Comment
        public ActionResult Index(string id)
        {
            return View(_commentService.GetCommentByPost(id));
        }
    }
}