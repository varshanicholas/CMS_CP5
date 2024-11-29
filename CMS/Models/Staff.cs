 admin_re
﻿using System.ComponentModel.DataAnnotations;

﻿using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Security.Cryptography.Xml;


namespace CMS.Models
{
    public class Staff
    {
         [Key]
         public int StaffId { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]
        [StringLength(20, ErrorMessage = "First Name cannot be more than 20 characters.")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]
        [StringLength(20, ErrorMessage = "Last Name cannot be more than 20 characters.")]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
       // [Range(typeof(DateTime), "1970-01-01", "2004-12-31", ErrorMessage = "Date of Birth must be between 1970 and 2004.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string BloodGroup { get; set; }
        [Required]
       // [Range(typeof(DateTime), "2024-11-25", "2024-11-28", ErrorMessage = "Joining Date must be between November 25 and November 28, 2024.")]
        public DateTime JoiningDate { get; set; }
        [Required]
        [Range(15000, 100000, ErrorMessage = "Salary must be between 15,000 and 100,000.")]
        public decimal Salary { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Experience must be between 0 and 20.")]
        public int Experience { get; set; }
        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phone Number must start with 6, 7, 8, or 9 and must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "Address cannot be longer than 40 characters.")]
        public string Address { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string DepartmentName { get; set; } // Dropdown option

        [Required]
        public string RoleName { get; set; } // Dropdown option
        






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
