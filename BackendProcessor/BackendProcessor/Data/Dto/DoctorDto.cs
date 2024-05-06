namespace BackendProcessor.Data.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? RegionId { get; set; }
        public bool IsPediatrician { get; set; }
        public int? SpecializationId { get; set; }
        public bool Nzok { get; set; }
        public List<int> InsuranceIds { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public string ImageUrl { get; set; }
    }
}
