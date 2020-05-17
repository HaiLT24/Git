using FA.JustBlog.DataAccessLayer.Service.InterfaceService;
using System.Web.Mvc;

namespace FA.JustBlog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;

        public HomeController(ICategoryService categoryService, IPostService postService, ITagService tagService)
        {
            _postService = postService;
            _tagService = tagService;
            ViewBag.Category = categoryService.GetAll();
        }

        public  ActionResult Index()
        {
            return RedirectToAction("Index", "Post");
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}