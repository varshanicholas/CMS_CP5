namespace CMS.Models
{
    public class AppointmentConfirmationViewModel
    {


        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int TokenNumber { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }

        public DateTime AppointmentDate { get; set; }
    }
}

   
