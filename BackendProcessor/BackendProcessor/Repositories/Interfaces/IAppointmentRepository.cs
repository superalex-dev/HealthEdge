using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task<Appointment> GetAppointmentAsync(int Id);
    Task<Appointment> CreateAppointmentAsync(Appointment appointment);
    Task<Appointment> EditAppointmentAsync(Appointment appointment);
    Task DeleteAppointmentAsync(int Id);
}