using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;
using FA.JustBlog.UI.Areas.Admin.ViewModels;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    public class CommentAdminController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentAdminController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateInput(false)]
        // GET: Admin/CommentAdmin
        public JsonResult Create(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    CommentId = commentViewModel.CommentId,
                    Name = commentViewModel.Name,
                    CommentHeader = commentViewModel.CommentHeader,
                    CommentText = commentViewModel.CommentText,
                    CommentTime = DateTime.Now,
                    Post = commentViewModel.Post,
                };
                var response = _commentService.Add(comment);

                if (response > 0)
                {
                    return Json(new
                    {
                        HasErrors = false,
                        Data = commentViewModel,
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            var errorMessages = ModelState.ToDictionary(
                ms => ms.Key,
                ms => ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            ).Where(ms => ms.Value.Any());

            return Json(new
            {
                HasErrors = true,
                Data = new { },
            }, JsonRequestBehavior.AllowGet);
        }
    }
}