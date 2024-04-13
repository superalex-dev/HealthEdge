using BackendProcessor.Data;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly HospitalDbContext _context;

    public AppointmentRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync() =>
        await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .ToListAsync();

    public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId) =>
        await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId) =>
        await _context.Appointments
            .Where(a => a.DoctorId == doctorId)
            .Include(a => a.Patient)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId) =>
        await _context.Appointments
            .Where(a => a.PatientId == patientId)
            .Include(a => a.Doctor)
            .ToListAsync();

    public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task UpdateAppointmentAsync(Appointment appointment)
    {
        _context.Entry(appointment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAppointmentAsync(int appointmentId)
    {
        var appointment = await GetAppointmentByIdAsync(appointmentId);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}