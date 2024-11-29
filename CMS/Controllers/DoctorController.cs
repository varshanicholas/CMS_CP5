using CMS.Repository;
using Microsoft.AspNetCore.Mvc;
namespace CMS.Controllers 
{
    public class DoctorController : Controller 
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public DoctorController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        // Action to display the landing page
        public IActionResult LandingPageDoctor() 
        {
            ////Test purpose with json
            //ViewBag.UserName = TempData["UserName"] ?? "Doctor"; //later comment this
            return View();
        }
        //public IActionResult TestRoute() //later comment this method
        //{
        //    return Content("Routing to DoctorController works!");
        //}

        // Action to display today's
        public IActionResult TodaysAppointments(int doctorId, string search = "")
        {
            // Assuming the doctor's ID is stored in the session 
            //TempData["doctorId"]="doctorId";
            doctorId = 2; 
            // Get today's appointments from the repository
            var appointments = _appointmentRepository.GetTodaysAppointments(doctorId);
            // If search term is provided, filter the appointments
            if (!string.IsNullOrEmpty(search)) 
            {
                appointments = appointments.Where(a => 
                a.PatientName.Contains(search, 
                StringComparison.OrdinalIgnoreCase) || 
                a.PatientId.ToString().Contains(search)).ToList();
            } 
            ViewData["Search"] = search; // Pass search term to the view 
            // Return the view with appointments
            return View(appointments); 
        }


    }
}