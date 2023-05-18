using Microsoft.AspNetCore.Mvc;
using MW.Application.Models.Membership;

namespace MW.Web.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserCreateModel userCreateModel)
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(UserUpdateModel userUpdateModel)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
