namespace CMS.Models
  {
    public class PrescriptionDetailModel
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<BillDetailsModel> MedicineDetails { get; set; } = new List<BillDetailsModel>();
        public decimal GrandTotal { get; set; }
    }


    public class BillDetailsModel
    {
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerStrip { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GST { get; set; }
    }


}

