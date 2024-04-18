using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class Patient
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "Date")]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        [StringLength(4)]
        public string BloodType { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        public int? UserId { get; set; }
        
        public User? User { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
