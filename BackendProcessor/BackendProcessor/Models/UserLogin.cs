using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
