using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class ConsultationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
