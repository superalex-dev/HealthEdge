using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProcessor.Models
{
    public class Doctor
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
        
        [StringLength(15)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [ForeignKey("RegionId")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public bool IsPediatrician { get; set; }

        [ForeignKey("SpecializationId")]
        public int? SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public bool Nzok { get; set; }

        //[ForeignKey("InsuranceId")]
        //public int? InsuranceId { get; set; }
        //public Insurance Insurance { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOfCreation { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<DoctorInsurance> DoctorInsurances { get; set; }
    }
}
