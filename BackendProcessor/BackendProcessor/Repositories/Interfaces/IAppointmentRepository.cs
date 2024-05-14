using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
    Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
    Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
    Task<Appointment> CreateAppointmentAsync(Appointment appointment);
    Task EditAppointmentAsync(Appointment appointment);
    Task DeleteAppointmentAsync(int appointmentId);
    Task<bool> IsAppointmentDateTaken(DateTime date);
    Task<Appointment> FindSoonestAvailableAppointment(int doctorId);
    Task<IEnumerable<Appointment>> GetPatientsByDoctorIdAsync(int doctorId);
}