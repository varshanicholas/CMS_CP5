using CMS.Models;
using CMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMS.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly ILabTestRepository _labTestRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public ConsultationController(
            IConsultationRepository consultationRepository,
            IMedicineRepository medicineRepository,
            ILabTestRepository labTestRepository,
            IAppointmentRepository appointmentRepository)
        {
            _consultationRepository = consultationRepository;
            _medicineRepository = medicineRepository;
            _labTestRepository = labTestRepository;
            _appointmentRepository = appointmentRepository;
        }

        public IActionResult StartConsultation(int appointmentId)
        {
            // Fetch the appointment details based on appointmentId
            var appointment = _appointmentRepository.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            var model = new ConsultationModels { AppointmentId = appointmentId, PatientId = appointment.PatientId };
            ViewBag.Medicines = _medicineRepository.GetAllMedicines();
            ViewBag.LabTests = _labTestRepository.GetAllLabTests();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddConsultationDetails(ConsultationModels details)
        {
            var history = new PatientHistory
            {
                PatientId = details.PatientId, 
                AppointmentId = details.AppointmentId,
                Symptoms = details.Symptoms, 
                Diagnosis = details.Diagnosis,
                MedicineId = details.MedicineId,
                Dosage = details.Dosage, 
                Frequency = details.Frequency, 
                TestId = details.TestId, 
                TreatmentPlan = details.TreatmentPlan, 
                Date = DateTime.Now
                //IsNewlyAdded = true
            };

            //_consultationRepository.AddConsultationDetails(history);
            //return RedirectToAction("StartConsultation", new { appointmentId = details.AppointmentId });

            _consultationRepository.AddConsultationDetails(history);
            
            return RedirectToAction("ViewPatientHistory", new { patientId = details.PatientId });
        }

        //public IActionResult ViewPatientHistory(int patientId)
        //{
        //    var histories = _consultationRepository.GetAllDetailsForHistory(patientId, true);
        //    return View(histories);
        //}

        public IActionResult ViewPatientHistory(int patientId) 
        {
            var histories = _consultationRepository.GetAllDetailsForHistory(patientId, true);
            if (histories.Any())
            { 
                var appointmentId = histories.First().AppointmentId;
                ViewBag.AppointmentId = appointmentId;
            }
            return View(histories); 
        }
        public IActionResult GeneratePatientReport(int patientId)
        {
            var histories = _consultationRepository.GetAllDetailsForHistory(patientId, true);
            return View(histories);
        }
    }
}
