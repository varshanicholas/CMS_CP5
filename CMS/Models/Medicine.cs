using CMS.Models;
namespace CMS.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }  // Unique ID for the medicine
        public string MedicineName { get; set; }  // Name of the medicine
        public string Description { get; set; }  // Optional description for the medicine
    }
}
