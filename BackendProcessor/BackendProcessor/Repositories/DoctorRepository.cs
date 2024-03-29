using BackendProcessor.Data;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendProcessor.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;

        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            var lastDoctor = await _context.Doctors
                .OrderByDescending(d => d.Username)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastDoctor != null && !string.IsNullOrEmpty(lastDoctor.Username))
            {
                var prefixLength = "healthedge".Length;
                if (lastDoctor.Username.Length > prefixLength)
                {
                    var lastNumberStr = lastDoctor.Username.Substring(prefixLength);
                    if (int.TryParse(lastNumberStr, out var lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
            }

            doctor.Username = $"healthedge{nextNumber:0000}";

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMultipleDoctorsAsync(IEnumerable<int> ids)
        {
            var doctors = _context.Doctors.Where(d => ids.Contains(d.Id));
            
            if (doctors.Any())
            {
                _context.Doctors.RemoveRange(doctors);
                await _context.SaveChangesAsync();
            }
        }
    }
}
