using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class AppointmentViewModel
    {

            public AppointmentViewModel()
            {
                // Set default appointment date to today's date
                AppointmentDate = DateTime.Now;
            }

            public int PatientId { get; set; }
            public string PatientName { get; set; }

            // For doctors dropdown
            public IEnumerable<SelectListItem> DoctorNames { get; set; }

            // This will store the selected doctor's id
            public int DoctorId { get; set; }

            // For specializations dropdown
            public IEnumerable<SelectListItem> SpecializationNames { get; set; }
            public int SpecializationId { get; set; }

            // Appointment Date
            [Required(ErrorMessage = "Appointment date is required.")]
            [DataType(DataType.Date)]
            public DateTime AppointmentDate { get; set; } = DateTime.Now; // Default to current date

            // Token Number (Auto-generated)
            public int TokenNumber { get; set; }

            public string AppointmentId { get; set; }

            public bool ConsultationStatus { get; set; } = false; // Default consultation status is false
        }
    }

