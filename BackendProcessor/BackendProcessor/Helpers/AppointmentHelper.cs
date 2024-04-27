using BackendProcessor.Data;
using BackendProcessor.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackendProcessor.Helpers
{
    public class AppointmentHelper
    {
        public void ValidateAppointmentTime(DateTime appointmentTime)
        {
            TimeSpan startOfWorkDay = new TimeSpan(8, 30, 0);
            TimeSpan endOfWorkDay = new TimeSpan(18, 30, 0);
            TimeSpan appointmentDuration = TimeSpan.FromMinutes(60);

            if (appointmentTime.TimeOfDay < startOfWorkDay ||
                appointmentTime.TimeOfDay > endOfWorkDay - appointmentDuration)
            {
                throw new ArgumentException("Appointment time is outside of working hours.");
            }
        }

        public async Task CheckForOverlappingAppointments(Appointment appointment, HospitalDbContext _context)
        {
            TimeSpan appointmentDuration = TimeSpan.FromMinutes(60);

            var existingAppointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.DoctorId == appointment.DoctorId &&
                                          a.AppointmentTime >= appointment.AppointmentTime &&
                                          a.AppointmentTime < appointment.AppointmentTime + appointmentDuration);

            if (existingAppointment != null)
            {
                throw new InvalidOperationException("This time slot is already booked.");
            }
        }

        public void ValidateAppointmentDetails(Appointment appointment)
        {
            if (string.IsNullOrEmpty(appointment.Reason) || string.IsNullOrEmpty(appointment.PaymentMethod))
            {
                throw new ArgumentException("Reason and PaymentMethod cannot be null or empty.");
            }
        }
    }
}