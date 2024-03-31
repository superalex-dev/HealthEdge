using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProcessor.Models
{
    public class Appointment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }

        [Required]
        [ForeignKey("DoctorId")]
        public int DoctorId { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        // Always 1H
        [Required]
        public DateTime AppointmentDate { get; set; }

        public int RoomNumber { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsCompleted { get; set; }

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
