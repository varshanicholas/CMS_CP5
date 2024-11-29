namespace CMS.Models
{
    public class Doctor
    {

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string  DoctorCode { get; set; }
        public decimal   ConsultationFee {  get; set; }
        public int SpecializationId {  get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    

}
}
