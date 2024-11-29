using CMS.Models;
using CMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly IPharmacistRepository _pharmacistRepository;

        public PharmacistController(IPharmacistRepository pharmacistRepository)
        {
            _pharmacistRepository = pharmacistRepository;
        }

        // GET: Medicine Prescriptions
        public IActionResult MedicinePrescriptions()
        {
            var prescriptions = _pharmacistRepository.GetMedicinePrescriptions();
            return View(prescriptions);
        }

        // GET: Appointment Details
        public IActionResult AppointmentDetails(int? id)
        {
            var details = _pharmacistRepository.GetAppointmentDetails(id);
            //var prescriptions = _pharmacistRepository.GetMedicinePrescriptions(appointmentId);

            ViewBag.Prescriptions = details;
            return View(details);
        }

        public IActionResult BillDetails(int? id)
        {
            var billDetails = _pharmacistRepository.GetBillDetails(id);
            return View(billDetails);
        }

        [HttpPost]
        public IActionResult PrintBill(PrescriptionDetailModel model)
        {
            // Logic to handle printing, or redirect to a printable version of the bill
            return RedirectToAction("PrintBillView", new { appointmentId = model.AppointmentId });
        }

        //public IActionResult  BillDetails(int appointmentId)
        //{
        //    var billDetails = _pharmacistRepository.GetBillDetails(appointmentId);
        //    return View(billDetails);
        //}

    }
}
