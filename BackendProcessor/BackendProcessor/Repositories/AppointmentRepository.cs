using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BackendProcessor.Data;

namespace BackendProcessor.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalDbContext _context;

        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task DeleteAppointmentAsync(int Id)
        {
            var appointment = await _context.Users.FindAsync(Id);
            if (appointment != null)
            {
                _context.Users.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Appointment> EditAppointmentAsync(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetAppointmentAsync(int Id)
        {
            return await _context.Appointments.FindAsync(Id);
        }
    }
}