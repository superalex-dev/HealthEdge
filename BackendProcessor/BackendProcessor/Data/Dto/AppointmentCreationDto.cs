namespace BackendProcessor.Data.Dto
{
    public class AppointmentCreationDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
    }
}
