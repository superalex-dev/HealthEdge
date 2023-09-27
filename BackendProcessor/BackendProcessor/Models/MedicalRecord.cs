using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class MedicalRecord
    {
        public int RecordId { get; set; }

        public int PatientId { get; set; }

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
