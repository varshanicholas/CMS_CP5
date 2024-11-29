//using Microsoft.AspNetCore.Mvc;

//namespace CMS.Controllers
//{
//    public class TestLoginController : Controller
//    {
//        // Simulated JSON data
//        private readonly List<User> Users = new List<User>
//        {
//            new User { RoleId = 3, Role = "Doctor", UserName = "Amit", Password = "Doctor@123" }
//        };

//        // Display the Login page
//        public IActionResult Index()
//        {
//            return View();
//        }

//        // Process Login Request
//        [HttpPost]
//        public IActionResult Login(string username, string password)
//        {
//            var user = Users.Find(u => u.UserName == username && u.Password == password);

//            if (user != null && user.Role == "Doctor")
//            {
//                TempData["UserName"] = user.UserName;
//                TempData["Role"] = user.Role;
//                return RedirectToAction("LandingPage", "Doctor");
//            }
//            else
//            {
//                ViewBag.ErrorMessage = "Invalid username or password.";
//                return View("Index");
//            }
//        }
//    }

//    // Temporary User Model
//    public class User
//    {
//        public int RoleId { get; set; }
//        public string Role { get; set; }
//        public string UserName { get; set; }
//        public string Password { get; set; }
//    }
//}