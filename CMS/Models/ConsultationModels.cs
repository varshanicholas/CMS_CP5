namespace CMS.Models
{
    public class ConsultationModels
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public int? MedicineId { get; set; }
        public string OtherMedicine { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int? TestId { get; set; }
        public string TreatmentPlan { get; set; }
        //public bool IsNewlyAdded { get; set; } // Identify new entries
    }
}
