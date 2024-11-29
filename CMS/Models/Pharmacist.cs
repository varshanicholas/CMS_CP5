namespace CMS.Models
{
    public class Pharmacist
    {
        public int AppointmentId { get; set; }
        public string Token { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string MedicineName { get; set; }
        public string DoctorName { get; set; }
        public int Quantity { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Frequency { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GST { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
