namespace BackendProcessor.Data.Dto
{
    public class DoctorSearchResultDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public int RegionId { get; set; }

        public bool IsPedeatrician { get; set; }

        public int SpecializationId { get; set; }

        public bool Nzok { get; set; }

        public int InsuranceId { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }
    }
}
