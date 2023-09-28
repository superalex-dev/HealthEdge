using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class VIPRoom
    {
        [Key]
        [Required]
        public int VIPRoomId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [StringLength(255)]
        public string SpecialAmenities { get; set; }

        public Room Room { get; set; }
    }
}
