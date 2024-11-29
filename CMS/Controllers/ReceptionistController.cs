using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class ReceptionistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
