using CMS.Models;

namespace CMS.Repository
{
    public interface IMedicineRepository
    {
        Medicine GetMedicineById(int medicineId);
        List<Medicine> GetAllMedicines();
    }
}
