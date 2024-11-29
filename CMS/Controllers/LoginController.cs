using CMS.Models;
using CMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class LoginController : Controller
    {
         private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    // GET: Login
    public IActionResult Index()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public async Task<IActionResult> Login(Login model)
    {
        if (ModelState.IsValid)
        {
            var (isValid, roleId, staffId) = await _loginRepository.ValidateLoginAsync(model.Username, model.Password);

            if (isValid)
            {
                TempData["RoleId"] = roleId;
                TempData["StaffId"] = staffId;

               
                switch (roleId)
                {
                    case 1: // Doctor
                        return RedirectToAction("DoctorDashboard", "Doctor");
                    case 2: // Administrator
                        return RedirectToAction("Index", "Admin");
                    case 3: // Receptionist
                        return RedirectToAction("ReceptionistDashboard", "Receptionist");
                    case 4: // Pharmacist
                        return RedirectToAction("PharmacistDashboard", "Pharmacist");
                    case 5: // Lab Technician
                        return RedirectToAction("LabTechnicianDashboard", "LabTechnician");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
            }
        }

        return View("Index");
    }
    }
}
