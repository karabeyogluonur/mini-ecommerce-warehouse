using Microsoft.AspNetCore.Mvc;

namespace MW.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}