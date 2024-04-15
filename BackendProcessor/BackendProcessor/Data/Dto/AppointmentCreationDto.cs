namespace BackendProcessor.Data.Dto
{
    public class AppointmentCreationDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public AppointmentCreationDto(int patientId, int doctorId, DateTime appointmentTime, string notes, string status)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentTime = appointmentTime;
            Notes = notes;
            Status = status;
        }
    }
}
