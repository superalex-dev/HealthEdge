namespace BackendProcessor.Data.Dto
{
    public class AppointmentCreationDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string PaymentMethod { get; set; }

        public AppointmentCreationDto()
        {
        }
    }
}
