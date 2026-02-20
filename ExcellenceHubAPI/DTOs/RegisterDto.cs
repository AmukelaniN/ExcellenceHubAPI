using System.ComponentModel.DataAnnotations;

namespace ExcellenceHubAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must be 10 digits")]

        public string ContactNumber { get; set; }

        [Required]
        [RegularExpression(
          @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$&])[A-Za-z\d!@#$&]{8,}$",
           ErrorMessage = "Password must be at least 8 characters long, contain at least one letter, one number, and one special character (!@#$&)."
         )]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }



    }
}
