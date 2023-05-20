using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MW.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
