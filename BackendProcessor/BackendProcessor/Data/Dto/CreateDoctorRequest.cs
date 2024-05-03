using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Data.Dto
{
    public class CreateDoctorRequest
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(15)]
        public string Username { get; set; }

        public int? RegionId { get; set; }

        public bool IsPediatrician { get; set; }

        public int? SpecializationId { get; set; }

        public bool Nzok { get; set; }

        public List<int> InsuranceIds { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string ImageUrl { get; set; }
    }
}
