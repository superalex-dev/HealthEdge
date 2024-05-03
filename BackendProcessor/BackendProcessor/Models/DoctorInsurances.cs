namespace BackendProcessor.Models
{
    public class DoctorInsurance
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
    }
}
