using Microsoft.AspNetCore.Mvc;

namespace MW.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}