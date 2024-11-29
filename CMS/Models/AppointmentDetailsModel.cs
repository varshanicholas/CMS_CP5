namespace CMS.Models
{
    public class AppointmentDetailsModel
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DoctorName { get; set; }

        // New Property for Medicine Details
        public List<MedicineDetailModel> Medicines { get; set; } = new List<MedicineDetailModel>();
    }

    public class MedicineDetailModel
    {
        public int SerialNo { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Frequency { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
