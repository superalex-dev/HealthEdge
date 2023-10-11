using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
