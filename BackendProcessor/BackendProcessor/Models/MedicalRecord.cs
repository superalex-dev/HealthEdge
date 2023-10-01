using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProcessor.Models
{
    public class MedicalRecord
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Diagnosis { get; set; }

        [Required]
        [StringLength(500)]
        public string Treatment { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }
    }
}
