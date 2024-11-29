admin_re
﻿using Microsoft.AspNetCore.Mvc;

﻿using CMS.Models;
using CMS.Repository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CMS.Controllers
{
    public class ReceptionistController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        IReceptionistRepository receptionistRepository = null;
        //private object _receptionistRepository;

        public ReceptionistController(IReceptionistRepository _receptionistRepository)
        {
            receptionistRepository = _receptionistRepository;
        }
        public IActionResult Index(string search)
        {
            // Fetch patients from the repository with search applied
            List<Patients> PatList = new List<Patients>();

            if (string.IsNullOrEmpty(search))
            {
                PatList = receptionistRepository.GetPatients().ToList(); // No search applied
                ViewData["IsSearchResult"] = false;  // No search result
            }
            else
            {
                // Filter by name or phone (adjust based on your data structure)
                PatList = receptionistRepository.GetPatients()
                                                .Where(p => p.PatientName.Contains(search) || p.PhoneNumber.Contains(search))
                                                .ToList();
                ViewData["IsSearchResult"] = true;  // Search result present
            }

            ViewData["Query"] = search;  // Store query in ViewData to retain it for the search input
            return View(PatList);
        }



        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Patients objPat)
        {
            if (ModelState.IsValid)
            {
                receptionistRepository.AddPatients(objPat);

            }
            return RedirectToAction("Index");
        }

        //Get the details of an employee
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Patients pat = receptionistRepository.GetPatientById(Id);
            if (pat == null)
            {
                return NotFound();
            }

            return View(pat);
        }


        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Patients patient = receptionistRepository.GetPatientById(Id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Patients objpat)
        {
            if (ModelState.IsValid)
            {
                receptionistRepository.UpdatePatients(objpat);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public JsonResult GetDoctorsBySpecialization(int specializationId)
        {
            var doctors = receptionistRepository.GetDoctorsBySpecialization(specializationId)
                .Select(d => new { value = d.DoctorId, text = d.DoctorName });

            return Json(doctors);
        }

        //[HttpGet]
        //public IActionResult BookAppointment(int id, int? specializationId)
        //{
        //    var viewModel = new AppointmentViewModel
        //    {
        //        SpecializationNames = receptionistRepository.GetSpecializations()
        //            .Select(s => new SelectListItem
        //            {
        //                Value = s.SpecializationId.ToString(),
        //                Text = s.SpecializationName
        //            }).ToList()
        //    };

        //    if (specializationId.HasValue)
        //    {
        //        viewModel.DoctorNames = receptionistRepository.GetDoctorsBySpecialization(specializationId.Value)
        //            .Select(d => new SelectListItem
        //            {
        //                Value = d.DoctorId.ToString(),
        //                Text = d.DoctorName
        //            }).ToList();
        //    }

        //    return View(viewModel);
        //}

        //[HttpPost]
        //public IActionResult BookAppointment(int id, Appointment model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var appointment = new Appointment
        //        {
        //            PatientId = id,
        //            DoctorId = model.DoctorId,
        //            AppointmentDate = model.AppointmentDate == default ? DateTime.Now : model.AppointmentDate,
        //            TokenNumber = model.TokenNumber,
        //            ConsultationStatus = model.ConsultationStatus
        //        };

        //        receptionistRepository.AddAppointment(appointment);

        //        return RedirectToAction("AppointmentConfirmation", new
        //        {
        //            appointmentId = appointment.AppointmentId,
        //            appointmentDate = appointment.AppointmentDate,
        //            patientName = model.PatientName,
        //            tokenNumber = appointment.TokenNumber,
        //            doctorName = receptionistRepository.GetDoctorById(appointment.DoctorId)?.DoctorName
        //        });
        //    }

        //    return View(model);
        //}
      

        public IActionResult BookAppointment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = receptionistRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }

            var departments = receptionistRepository.GetDepartments();
            ViewBag.Departments = departments;

            var model = new AppointmentViewModel
            {
                PatientId = patient.PatientId,
                PatientName = patient.PatientName
            };

            return View(model);
        }

        // GET: BookAppointment
        [HttpGet]
        public IActionResult BookAppointment(int id, int? specializationId)
        {
            var viewModel = new AppointmentViewModel();

            // Fetch specializations and add them to the view model
            var specializations = receptionistRepository.GetSpecializations();
            viewModel.SpecializationNames = specializations.Select(s => new SelectListItem
            {
                Value = s.SpecializationId.ToString(),
                Text = s.SpecializationName
            });

            // Fetch doctors if a specialization is selected
            if (specializationId.HasValue)
            {
                var doctors = receptionistRepository.GetDoctorsBySpecialization(specializationId.Value);
                viewModel.DoctorNames = doctors.Select(d => new SelectListItem
                {
                    Value = d.DoctorId.ToString(),
                    Text = d.DoctorName
                });
            }

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult BookAppointment(int id, AppointmentViewModel model)
        {
            // Set the current date if not set
            if (model.AppointmentDate == default)
            {
                model.AppointmentDate = DateTime.Now;
            }

            // Repopulate specializations
            var specializations = receptionistRepository.GetSpecializations();
            model.SpecializationNames = new SelectList(specializations, "SpecializationId", "SpecializationName");

            // Repopulate doctors if a specialization is selected
            if (model.SpecializationId > 0)
            {
                var doctors = receptionistRepository.GetDoctorsBySpecialization(model.SpecializationId);
                model.DoctorNames = new SelectList(doctors, "DoctorId", "DoctorName");
            }

            /* // if (ModelState.IsValid)
              //{
                  // Save appointment
                  //var appointment = new Appointment
                  {
                      PatientId = model.PatientId,
                      DoctorId = model.DoctorId,
                      AppointmentDate = model.AppointmentDate,
                      TokenNumber = model.TokenNumber,
                      ConsultationStatus = model.ConsultationStatus
                  };*/
            Appointment appointment = new Appointment();
            appointment.PatientId = id;
            appointment.DoctorId = model.DoctorId;
            appointment.AppointmentDate = model.AppointmentDate;
            appointment.TokenNumber = model.TokenNumber;
            appointment.ConsultationStatus = model.ConsultationStatus;


            receptionistRepository.AddAppointment(appointment);

            // Fetch doctor details for confirmation
            var doctor = receptionistRepository.GetDoctorById(appointment.DoctorId);
            var doctorName = doctor.DoctorName;

            // Redirect to confirmation
            return RedirectToAction("AppointmentConfirmation", new
            {
                appointmentId = appointment.AppointmentId,
                appointmentDate = appointment.AppointmentDate,
                patientName = model.PatientName,
                tokenNumber = appointment.TokenNumber,
                doctorId = appointment.DoctorId,
                doctorName = doctor.DoctorName

            });
            //}

            return View(model);
        }




        // Fetch the list of departments
        private IEnumerable<Department> GetDepartments()
        {
            //fetch departments from the database
            return receptionistRepository.GetDepartments();
        }

        //[HttpGet]
        //public JsonResult GetDoctorsBySpecialization(int specializationId)
        //{
        //    var doctors = _receptionistRepository.GetDoctorsBySpecialization(specializationId);

        //    var result = doctors.Select(d => new
        //    {
        //        value = d.DoctorId,
        //        text = d.DoctorName
        //    });

        //    return Json(result);
        //}


        [HttpGet]
        public IActionResult GetDoctorsByDepartment(int departmentId)
        {
            var doctors = receptionistRepository.GetDoctorsByDepartment(departmentId);
            var doctorList = doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = d.DoctorName
            });

            return Json(doctorList);
        }




        public JsonResult GenerateTokenForDoctor(int doctorId, DateTime appointmentDate)
        {
            try
            {
                int tokenNumber = receptionistRepository.GenerateTokenForDoctor(doctorId, appointmentDate); // Call the repository method
                return Json(new { success = true, token = tokenNumber }); // Wrap the token in an object
            }
            catch (InvalidOperationException ex)
            {
                // Handle the case when the maximum token limit is reached
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                return Json(new { success = false, message = "An error occurred while generating the token." });
            }
        }


        [HttpGet]
        public IActionResult AppointmentConfirmation(int appointmentId, string patientName, int tokenNumber, int doctorId, string doctorName, DateTime appointmentDate)
        {
            // Fetch appointment details
            var appointment = receptionistRepository.GetAppointmentDetails(appointmentId);
            var doctor = receptionistRepository.GetDoctorById(doctorId);

            // Fetch staff details using the staffId from doctor
            //var staff = _receptionistRepository.GetStaffById(doctor.StaffId);

            // Prepare confirmation view model
            AppointmentConfirmationViewModel confirmationViewModel = new AppointmentConfirmationViewModel
            {
                AppointmentId = appointment.AppointmentId,
                PatientName = appointment.PatientName, // Assuming PatientName is passed correctly
                TokenNumber = appointment.TokenNumber,
                DoctorId = doctor.DoctorId,
                //DoctorName = $"{staff.FirstName} {staff.LastName}", // Combine first and last name from Staff table
                DoctorName = doctor.DoctorName,
                AppointmentDate = appointment.AppointmentDate,
            };

            return View(confirmationViewModel);
        }



    }
}
