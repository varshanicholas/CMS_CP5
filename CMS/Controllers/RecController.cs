using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class RecController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
