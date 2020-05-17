﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Core.Models;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    public class TagAdminController : Controller
    {
        private readonly ITagService _tagService;

        public TagAdminController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: Admin/TagAdmin
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

            ViewBag.CurrentFilter = searchString;

            Expression<Func<Tag, bool>> filter = null;
            Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy;

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
            var tags = _tagService.GetWithPagingAsync(filter, orderBy, pageIndex: pageIndex, pageSize: pageSize ?? 10);

            return View(await tags);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                if (_tagService.Add(tag) <= 0)
                    return RedirectToAction("Index");
            }

            return View(tag);
        }

        public ActionResult Details(string id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(_tagService.Find(id));
        }

        public ActionResult Edit(string id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _tagService.Find(id);
            if (tag is null)
            {
                return HttpNotFound();
            }
            return View(tag); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                if (_tagService.Update(tag))
                {
                    return RedirectToAction("Index");
                }
            }

            return View(tag); ;
        }


        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            _tagService.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tagService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}