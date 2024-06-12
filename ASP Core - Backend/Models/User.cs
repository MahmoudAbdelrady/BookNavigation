using Microsoft.AspNetCore.Identity;

namespace aspcorebackend.Models {
    public class User : IdentityUser<int> {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; }

        public User() {
            this.CreatedAt = DateTime.Now;
        }
    }
}
