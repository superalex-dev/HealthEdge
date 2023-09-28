using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Key]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }
    }
}
