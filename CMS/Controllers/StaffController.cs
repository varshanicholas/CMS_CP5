using CMS.Models;
using CMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class StaffController : Controller
    {
         private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }


        public IActionResult StaffList()
        {
            try
            {
                var staffList = _staffRepository.GetStaffList().ToList();
                return View(staffList);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Unable to fetch staff list.";
                return View("Error", new { message = ex.Message });
            }
        }



        [HttpGet]
        public IActionResult AddStaff()
        {
            try
            {
                ViewBag.Departments = _staffRepository.GetDepartments();
                ViewBag.Roles = _staffRepository.GetRoles();
                return View();
            }
            catch (Exception ex)
            {
                
                TempData["Error"] = "Unable to load data for adding staff.";
                return View("Error", ex.Message);
            }
        }

        /// <summary>
        /// Processes the form submission for adding new staff.
        /// </summary>
        /// <param name="staff">The staff object containing form data.</param>
        /// <returns>Redirects to AddStaff on success or reloads the form with errors.</returns>
        [HttpPost]
        public IActionResult AddStaff(Staff staff)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _staffRepository.AddStaff(staff);
                    TempData["Message"] = "Staff added successfully!";
                    return RedirectToAction("AddStaff");
                }
                catch (Exception ex)
                {
                    // Log the error and display a meaningful message to the user
                    TempData["Error"] = "Error occurred while adding staff.";
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            // Reload the dropdowns if the model state is invalid or an error occurs
            ViewBag.Departments = _staffRepository.GetDepartments();
            ViewBag.Roles = _staffRepository.GetRoles();
            return View(staff);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = _staffRepository.GetStaffById(id);

            if (staff == null)
            {
                return NotFound();
            }

            return View(staff); // Passing the model to the view
        }


        public IActionResult Enable(int id)
        {
            var staff = _staffRepository.GetStaffById(id);
            if (staff != null)
            {
                staff.IsActive = true; // Enable the staff member
                _staffRepository.UpdateStaff(staff); // Save changes
                TempData["SuccessMessage"] = "Staff member enabled successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Staff member not found!";
            }
            return RedirectToAction("StaffList");
        }

        // Disable a staff member
        [HttpPost]
        public IActionResult Disable(int id)
        {
            var staff = _staffRepository.GetStaffById(id);
            if (staff != null)
            {
                staff.IsActive = false; // Disable the staff member
                _staffRepository.UpdateStaff(staff); // Save changes
                TempData["SuccessMessage"] = "Staff member disabled successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Staff member not found!";
            }
            return RedirectToAction("StaffList");
        }
        public IActionResult Edit(int id)
        {
            var staffList = _staffRepository.GetStaffById(id);
            if (staffList == null)
            {
                return NotFound();
            }
            return View(staffList);
        }

        // Action to handle updating a medicine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Staff staffList)
        {
            if (id != staffList.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _staffRepository.UpdateStaff(staffList);
                TempData["SuccessMessage"] = "Staff updated successfully!";
                return RedirectToAction("StaffList");
            }
            return View(staffList);
        }
        [HttpGet]
        public IActionResult StaffList(string PhoneNumber = null, string name = null)
        {
            // Get the search results from the repository
            var staffList = _staffRepository.SearchStaffByPhoneNumberAndName(PhoneNumber, name);

            // Return the search results to the view
            return View(staffList);
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

       
    }
}
