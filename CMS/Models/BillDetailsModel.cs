namespace CMS.Models
{
    namespace CMS.Models
    {
        public class BillDetailsModel
        {
            public int AppointmentId { get; set; }
            public string PatientName { get; set; }
            public List<BillItemModel> BillItems { get; set; } = new List<BillItemModel>();
            public decimal GrandTotal { get; set; }
        }

        public class BillItemModel
        {
            public string MedicineName { get; set; }
            public int Quantity { get; set; }
            public decimal PricePerUnit { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal GST { get; set; }
            public decimal FinalAmount { get; set; }
        }
    }

}
