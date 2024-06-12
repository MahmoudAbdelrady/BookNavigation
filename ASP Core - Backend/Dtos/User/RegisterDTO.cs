using System.ComponentModel.DataAnnotations;

namespace aspcorebackend.Dtos.User {
    public class RegisterDTO {
        [RegularExpression(@"^[a-zA-Z0-9_]{4,64}$", ErrorMessage = "Username can't contain any special characters")]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters")]
        [MaxLength(64, ErrorMessage = "Username can't exceed 64 characters")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            ErrorMessage = "Password must be 8 characters at least and contains: 1 Uppercase letter, 1 Lowercase letter, 1 Special character, 1 digit")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
