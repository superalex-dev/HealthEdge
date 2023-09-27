using System.ComponentModel.DataAnnotations;

namespace BackendProcessor.Models
{
    public class VIPRoom
    {
        public int VIPRoomId { get; set; }

        public int RoomId { get; set; }

        [StringLength(255)]
        public string SpecialAmenities { get; set; }

        public Room Room { get; set; }
    }
}
