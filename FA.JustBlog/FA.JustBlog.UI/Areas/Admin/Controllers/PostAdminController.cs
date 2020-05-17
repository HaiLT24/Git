using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;
using FA.JustBlog.UI.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    public class PostAdminController : Controller
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;

        public PostAdminController(IPostService postService, ICategoryService categoryService, ITagService tagService)
        {
            _postService = postService;
            _tagService = tagService;

            ViewBag.CategoryId = new SelectList(categoryService.GetAll(), "Id", "Name");
        }

        // GET: Admin/PostAdmin
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
            ViewData["CategorySortParm"] = sortOrder == "category" ? "category_desc" : "category";
            ViewData["PostedSortParm"] = sortOrder == "posted" ? "posted_desc" : "posted";
            ViewData["ViewCountSortParm"] = sortOrder == "viewCount" ? "viewCount_desc" : "viewCount";
            ViewData["RateSortParm"] = sortOrder == "rate" ? "rate_desc" : "rate";
            ViewData["CommentSortParm"] = sortOrder == "comment" ? "comment_desc" : "comment";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentSort = page;
            ViewBag.CurrentFilter = searchString;

            Expression<Func<Post, bool>> filter = null;
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy;

            if (!String.IsNullOrEmpty(searchString))
            {
                filter = s => s.Title.Contains(searchString) || s.Category.Name.Contains(searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(x => x.Title);
                    break;
                case "posted_desc":
                    orderBy = b => b.OrderByDescending(x => x.PostedOn);
                    break;
                case "posted":
                    orderBy = b => b.OrderBy(x => x.PostedOn);
                    break;
                case "category_desc":
                    orderBy = b => b.OrderByDescending(x => x.Category.Name);
                    break;
                case "category":
                    orderBy = b => b.OrderBy(x => x.Category.Name);
                    break;
                case "viewCount_desc":
                    orderBy = b => b.OrderByDescending(x => x.ViewCount);
                    break;
                case "viewCount":
                    orderBy = b => b.OrderBy(x => x.ViewCount);
                    break;
                case "rate_desc":
                    orderBy = b => b.OrderByDescending(x => (x.TotalRate / x.RateCount));
                    break;
                case "rate":
                    orderBy = b => b.OrderBy(x => (x.TotalRate / x.RateCount));
                    break;
                case "comment_desc":
                    orderBy = b => b.OrderByDescending(x => x.Comments.Count);
                    break;
                case "comment":
                    orderBy = b => b.OrderBy(x => x.Comments.Count);
                    break;
                default:
                    orderBy = b => b.OrderBy(x => x.Title);
                    break;
            }

            var pageIndex = (page ?? 1);
            var posts = _postService.GetWithPagingAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize ?? 10);

            return View(await posts);
        }

        public ActionResult Create()
        {
            var listTag = _tagService.GetAll().ToList();
            var postViewModel = new PostAddViewModel()
            {
                Tags = listTag
            };

            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PostAddViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var tags = new List<Tag>();
                foreach (var tag in postViewModel.TagsId)
                {
                    tags.Add(_tagService.Find(tag));
                }

                postViewModel.Tags = tags;
                var post = new Post()
                {
                    Id = postViewModel.Id,
                    Title = postViewModel.Title,
                    ShortDescription = postViewModel.ShortDescription,
                    PostContent = postViewModel.PostContent,
                    PostedOn = DateTime.Now,
                    Publisher = postViewModel.Publisher,
                    UrlSlug = postViewModel.UrlSlug,
                    ViewCount = postViewModel.ViewCount,
                    RateCount = postViewModel.RateCount,
                    TotalRate = postViewModel.TotalRate,
                    CategoryId = postViewModel.CategoryId,
                    Tags = postViewModel.Tags
                };
                if (_postService.Add(post) > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            var listTag = _tagService.GetAll();
            postViewModel.Tags = listTag.ToList();

            return View(postViewModel);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(_postService.Find(id));
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = _postService.Find(id);
            var listTag = _tagService.GetAll().ToList();

            var postViewModel = new PostAddViewModel
            {
                Id = post.Id,
                Tags = listTag,
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                PostContent = post.PostContent,
                Publisher = post.Publisher,
                ViewCount = post.ViewCount,
                RateCount = post.RateCount,
                TotalRate = post.TotalRate,
                UrlSlug = post.UrlSlug,
                CategoryId = post.CategoryId
            };

            return View(postViewModel); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PostAddViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = _postService.Find(postViewModel.Id);

                post.Title = postViewModel.Title;
                post.ShortDescription = postViewModel.ShortDescription;
                post.PostContent = postViewModel.PostContent;
                post.Publisher = postViewModel.Publisher;
                post.UrlSlug = postViewModel.UrlSlug;
                post.CategoryId = postViewModel.CategoryId;
                post.ViewCount = postViewModel.ViewCount;
                post.RateCount = postViewModel.RateCount;
                post.TotalRate = post.TotalRate;
                post.Tags = postViewModel.Tags;

                if (_postService.Update(post))
                {
                    return RedirectToAction("Index");
                }

            }
            var listTag = _tagService.GetAll();
            postViewModel.Tags = listTag.ToList();

            return View(postViewModel); ;
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            _postService.DeleteById(id);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult ChangePublisher(string id)
        {
            var result = _postService.ChangePublisher(id);

            return Json(new
            {
                publisher = result
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _postService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}