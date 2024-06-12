using System.ComponentModel.DataAnnotations;

namespace aspcorebackend.Dtos.User {
    public class LoginDTO {
        [Required(ErrorMessage = "Username or email is required")]
        public string UsernameOrEmail { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
