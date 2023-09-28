using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class RoomCost
    {
        [Key]
        [Required]
        public int RoomCostId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DailyRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal WeeklyRate { get; set; }
    }
}
