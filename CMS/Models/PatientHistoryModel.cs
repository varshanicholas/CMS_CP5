namespace CMS.Models
{
    public class PatientHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; } // Add this property
        public int? MedicineId { get; set; } // Add this property
        public int? TestId { get; set; } // Add this property
        public string Symptoms { get; set; } 
        public string Diagnosis { get; set; } 
      
        public string Medicine { get; set; } 
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string LabTest { get; set; }
        public string TreatmentPlan { get; set; } // Add this property
        public DateTime Date { get; set; } 
        public int CreatedBy { get; set; } 

    }
}
