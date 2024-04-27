﻿using BackendProcessor.Data;
using BackendProcessor.Helpers;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        _appointmentHelper.ValidateAppointmentTime(appointment.AppointmentTime);

        await _appointmentHelper.CheckForOverlappingAppointments(appointment, _context);

        _appointmentHelper.ValidateAppointmentDetails(appointment);

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

    public async Task<DateTime?> GetAvailaleSlots(int doctorId, DateTime desiredDate)
    {
        TimeSpan startOfWorkDay = new TimeSpan(8, 30, 0);
        TimeSpan endOfWorkDay = new TimeSpan(18, 30, 0);
        TimeSpan appointmentDuration = TimeSpan.FromMinutes(60);

        DateTime currentSlot = desiredDate.Date + startOfWorkDay;

        var appointments = await _context.Appointments
            .Where(a => a.DoctorId == doctorId &&
                        a.AppointmentTime.Date == desiredDate.Date)
            .ToListAsync();

        while (currentSlot.TimeOfDay <= endOfWorkDay - appointmentDuration)
        {
            if (!appointments.Any(a => a.AppointmentTime == currentSlot))
            {
                return currentSlot;
            }
            currentSlot = currentSlot.Add(appointmentDuration);
        }

        return null;
    }
}