using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomType { get; set; }

        public bool IsOccupied { get; set; }
    }
}
