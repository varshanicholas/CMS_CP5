
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
    {
        public class Patients
        {
        [Key]
            public int PatientId { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s]+$", ErrorMessage = "Name must start with letters & must contain only letters and space")]
        public string PatientName { get; set; }

            [Required(ErrorMessage = "Date of Birth is required.")]
            [CustomValidation(typeof(Patients), nameof(ValidateDOB))]
            public DateTime DOB { get; set; }

            [Required(ErrorMessage = "Gender is required.")]
            public string Gender { get; set; }

            [Required(ErrorMessage = "Blood group is required.")]
            [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood group format. Example: A+, O-")]
            public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phone Number must be a valid 10-digit number starting with 6-9.")]

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s]+$", ErrorMessage = "Address must start with letters & must contain only letters and space")]
        public string Address { get; set; }

            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string? Email { get; set; } // Nullable Email

        public DateTime DateOfRegistration { get; set; }  // Nullable DateTime

        // Static validation method for DOB
        public static ValidationResult ValidateDOB(DateTime dob, ValidationContext context)
            {
                if (dob > DateTime.Now)
                {
                    return new ValidationResult("Date of Birth cannot be a future date.");
                }
                return ValidationResult.Success!;
            }
        }
    }



