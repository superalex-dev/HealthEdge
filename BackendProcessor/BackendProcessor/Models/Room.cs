using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProcessor.Models
{
    public class Room
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; }

        [Required]
        public int RoomCapacity { get; set; }

        [Required]
        public int RoomPrice { get; set; }

        [Required]
        public bool IsOccupied { get; set; }
    }
}
