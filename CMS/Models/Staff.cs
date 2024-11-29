using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Security.Cryptography.Xml;

namespace CMS.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        public int StaffCode { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Date  DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public int JoiningDate { get; set; }
        public int Salary { get; set; }
        public int Experience { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public string IsActive { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
       


    }
}
