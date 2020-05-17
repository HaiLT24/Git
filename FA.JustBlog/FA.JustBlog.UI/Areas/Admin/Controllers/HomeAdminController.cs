using System.Web.Mvc;

namespace FA.JustBlog.UI.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}