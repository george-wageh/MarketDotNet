using System.ComponentModel.DataAnnotations;

namespace SharedLib.DTO
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Name must be at least 8 characters long.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone must be 11 characters long.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", 
            ErrorMessage = "Phone number must start with 010, 011, 012, or 015 and contain 11 digits.")]
        public string Phone { get; set; }
    }
}
