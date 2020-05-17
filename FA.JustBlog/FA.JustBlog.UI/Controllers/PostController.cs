using System;
using System.Collections.Generic;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using FA.JustBlog.Core.Models;
using FA.JustBlog.UI.Areas.Admin.ViewModels;

namespace FA.JustBlog.UI.Controllers
{
    public class PostController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly ICommentService _commentService;

        public PostController(ICategoryService categoryService, IPostService postService, ITagService tagService, ICommentService commentService)
        {
            _postService = postService;
            _tagService = tagService;
            _categoryService = categoryService;
            _commentService = commentService;
            ViewBag.Category = _categoryService.GetAll();
        }

        // GET: PostDefault
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem {Text = @"5", Value = "5"},
                new SelectListItem {Text = @"10", Value = "10"},
                new SelectListItem {Text = @"20", Value = "20"},
                new SelectListItem {Text = @"50", Value = "50"},
                new SelectListItem {Text = @"100", Value = "100"},
            };

            foreach (var item in items.Where(item => item.Value == pageSize.ToString()))
            {
                item.Selected = true;
            }

            ViewBag.size = items;
            ViewData["CurrentSize"] = pageSize ?? 10;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PostedOnParm"] = sortOrder == "postedOn" ? "postedOn_desc" : "postedOn";
            ViewData["ModifiedParm"] = sortOrder == "modified" ? "modified_desc" : "modified";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            } 

            Expression<Func<Post, bool>> filter = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy;

            if (!String.IsNullOrEmpty(searchString))
            {
                filter = s => s.Title.Contains(searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(x => x.Title);
                    break;
                case "postedOn_desc":
                    orderBy = b => b.OrderByDescending(x => x.PostedOn);
                    break;
                case "postedOn":
                    orderBy = b => b.OrderBy(x => x.PostedOn);
                    break;
                case "modified_desc":
                    orderBy = b => b.OrderByDescending(x => x.Modified);
                    break;
                case "modified":
                    orderBy = b => b.OrderBy(x => x.Modified);
                    break;
                default:
                    orderBy = b => b.OrderBy(x => x.Title);
                    break;
            }

            var pageIndex = (page ?? 1);
            var posts = _postService.GetWithPagingAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize ?? 10);

            return View(await posts);
        }
        public ActionResult Detail(string id)
        {
            return View(_postService.Find(id));
        }

        public ActionResult LatestPosts()
        {
            var listPostLatestPosts = _postService.GetLatestPost(5);
            return View(listPostLatestPosts);
        }


        [ChildActionOnly]
        public ActionResult AboutCard()
        {
            return PartialView("AboutCard");
        }

        public ActionResult MostViewedPosts()
        {
            var listHighestPosts = _postService.GetHighestPosts(5).ToList();
            return PartialView("_ListPosts", listHighestPosts);
        }

        public ActionResult LatestPostsinside()
        {
            var listLatePosts = _postService.GetLatestPost(5).ToList();
            return PartialView("_ListPosts", listLatePosts);
        }

        public ActionResult PostByCategory(string categoryId)
        {

            var category = _categoryService.Find(categoryId);
            var listPostByCategory = _postService.GetPostsForCategory(category.Name);

            ViewBag.Title = category.Name;

            return View(listPostByCategory);
        }

        public ActionResult PostsByTag(string tagId)
        {
            var tag = _tagService.Find(tagId);
            var listPostByTag = _postService.GetPostsForTag(tag.Name);

            ViewBag.Title = tag.Name;
            return View(listPostByTag);
        }

        public JsonResult CommentDetail(string id)
        {
            
            var data = _commentService.GetCommentByPost(id);
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CreateComment(string id, string name, string comment)
        {
            var commentViewModel = new CommentViewModel()
            {
                Name = name,
                CommentText = comment,
                PostId = id
            };

            if (ModelState.IsValid)
            {
                Comment comments = new Comment()
                {
                    CommentId = commentViewModel.CommentId,
                    Name = commentViewModel.Name,
                    CommentHeader = commentViewModel.CommentHeader,
                    CommentText = commentViewModel.CommentText,
                    CommentTime = DateTime.Now,
                    PostId = id
                };
                var response = _commentService.Add(comments);

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