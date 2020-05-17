using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    public class CategoryAdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public CategoryAdminController(ICategoryService categoryService, IPostService postService)
        {
            _categoryService = categoryService;
            _postService = postService;
            ViewBag.PageSize = 10;
        }

        // GET: Admin/CategoryAdmin
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
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CountSortParm"] = sortOrder == "count" ? "count_desc" : "count";
            
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

            Expression<Func<Category, bool>> filter = null;
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy;

            if (!String.IsNullOrEmpty(searchString))
            {
                filter = s => s.Name.Contains(searchString) || s.Description.Contains(searchString);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = b => b.OrderByDescending(x => x.Name);
                    break;
                case "count_desc":
                    orderBy = b => b.OrderByDescending(x => x.Posts.Count);
                    break;
                case "count":
                    orderBy = b => b.OrderBy(x => x.Posts.Count);
                    break;
                default:
                    orderBy = b => b.OrderBy(x => x.Name);
                    break;
            }

            var pageIndex = (page ?? 1);
            var categories = _categoryService.GetWithPagingAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize ?? 10);

            return View(await categories);

        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (_categoryService.Add(category) > 0)
                {
                    return RedirectToAction("Index"); ;
                }
            }

            return View(category);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(_categoryService.Find(id));
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _categoryService.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (_categoryService.Update(category))
            {
                return RedirectToAction("Index");
            }

            return View(category); ;
        }


        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            _categoryService.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _categoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}