using CMS.Models;

namespace CMS.Repository
{
    public interface IStaffRepository
    {
         
        /// <param name="staff">The staff object containing all staff details.</param>
        public void AddStaff(Staff staff);

        
       public List<string> GetDepartments();

        
      public List<String> GetRoles();

        
       public IEnumerable<Staff> GetStaffList();
        public Staff GetStaffById(int? id);
        public void EnableStaff(int id);

        // Disable a medicine
        public void DisableStaff(int id);
        public void UpdateStaff(Staff staff);
        public IEnumerable<Staff> SearchStaffByPhoneNumberAndName(string PhoneNumber = null, string Name = null);




    }
}
