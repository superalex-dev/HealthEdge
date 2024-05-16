using BackendProcessor.Data;
using BackendProcessor.Helpers;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using BackendProcessor.Data.Dto;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly HospitalDbContext _context;
    private readonly AppointmentHelper _appointmentHelper;  

    public AppointmentRepository(HospitalDbContext context, AppointmentHelper appointmentHelper)
    {
        _context = context;        
        _appointmentHelper = appointmentHelper;

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
        if (appointment == null)
        {
            throw new ArgumentNullException(nameof(appointment));
        }

        //_appointmentHelper.ValidateAppointmentTime(appointment.AppointmentTime);

       // await _appointmentHelper.CheckForOverlappingAppointments(appointment, _context);

        //_appointmentHelper.ValidateAppointmentDetails(appointment);

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

    public async Task<Appointment> FindSoonestAvailableAppointment(int doctorId)
    {
        return await _context.Appointments
            .Where(a => a.DoctorId == doctorId)
            .OrderBy(a => a.AppointmentTime)
            .LastAsync();
    }

    public async Task<bool> IsAppointmentDateTaken(DateTime date)
    {
        return await _context.Appointments
            .AnyAsync(a => a.AppointmentTime == date);
    }

    //public async Task<IEnumerable<Patient>> GetPatientsByDoctorIdAsync(int doctorId)
    //{
    //    //var todayStartUtc = DateTime.UtcNow.Date;
    //    //var todayEndUtc = todayStartUtc.AddDays(1);

    //    //var appointments = await _context.Appointments
    //    //    .Where(a => a.DoctorId == doctorId &&
    //    //                a.AppointmentTime.ToUniversalTime() >= todayStartUtc &&
    //    //                a.AppointmentTime.ToUniversalTime() < todayEndUtc)
    //    //    .Include(a => a.Patient)
    //    //    .ToListAsync();

    //    //return appointments.Select(a => new PatientDto
    //    //{
    //    //    Id = a.Id,
    //    //    FirstName = a.Patient.FirstName,
    //    //    LastName = a.Patient.LastName,
    //    //    UserName = a.Patient.UserName,
    //    //    Email = a.Patient.Email,
    //    //    DateOfBirth = a.Patient.DateOfBirth,
    //    //    Gender = a.Patient.Gender,
    //    //    BloodType = a.Patient.BloodType,
    //    //}); ;
    //}
}