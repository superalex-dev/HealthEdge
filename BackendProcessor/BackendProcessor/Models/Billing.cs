using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{

    public class Billing
    {
        public int BillId { get; set; }

        public int PatientId { get; set; }

        [Required]
        public DateTime BillDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public Patient Patient { get; set; }
    }
}
