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
        TimeSpan startOfWorkDay = new TimeSpan(8, 30, 0);
        TimeSpan endOfWorkDay = new TimeSpan(18, 30, 0);
        
        if (appointment.AppointmentTime.TimeOfDay < startOfWorkDay || 
            appointment.AppointmentTime.TimeOfDay > endOfWorkDay)
        {
            throw new InvalidOperationException("Appointment time is outside of working hours.");
        }

        var existingAppointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.DoctorId == appointment.DoctorId &&
                                      a.AppointmentTime == appointment.AppointmentTime);

        if (existingAppointment != null)
        {
            throw new InvalidOperationException("This time slot is already booked.");
        }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return appointment;
    }

    public async Task EditAppointmentAsync(Appointment appointment)
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
    
    public async Task<IEnumerable<DateTime>> GetAvailableSlots(int doctorId, DateTime desiredDate)
    {
        TimeSpan startOfWorkDay = new TimeSpan(8, 30, 0);
        TimeSpan endOfWorkDay = new TimeSpan(18, 30, 0);
        TimeSpan appointmentDuration = TimeSpan.FromMinutes(60);

        List<DateTime> availableSlots = new List<DateTime>();
        
        DateTime currentSlot = desiredDate.Date + startOfWorkDay;
        
        var appointments = await _context.Appointments
            .Where(a => a.DoctorId == doctorId && 
                        a.AppointmentTime.Date == desiredDate.Date)
            .ToListAsync();

        while (currentSlot.TimeOfDay <= endOfWorkDay - appointmentDuration)
        {
            if (!appointments.Any(a => a.AppointmentTime == currentSlot))
            {
                availableSlots.Add(currentSlot);
            }
            currentSlot = currentSlot.Add(appointmentDuration);
        }

        return availableSlots;
    }
}