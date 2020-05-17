using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Core.Repository;
using FA.JustBlog.Core.Repository.InterfaceRepositories;
using FA.JustBlog.DataAccessLayer.Service.InterfaceService;

namespace FA.JustBlog.UI.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        
        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult PopularTags()
        {
            _tagService.CalculateCountTags();
            var listTag = _tagService.GetTagForCount(10);
            return PartialView("_PopularTags", listTag);
        }
    }
}