namespace CMS.Models
{
    public class MedicinePrescriptionModel
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Medicine { get; set; }
        public string DoctorName { get; set; }
    }
}
