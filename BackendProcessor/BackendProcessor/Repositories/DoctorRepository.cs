using BackendProcessor.Data;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor, IEnumerable<int> insuranceIds)
        {
            await _context.Doctors.AddAsync(doctor);

            foreach (var insuranceId in insuranceIds)
            {
                var doctorInsurance = new DoctorInsurance
                {
                    Doctor = doctor,
                    InsuranceId = insuranceId
                };

                await _context.DoctorInsurances.AddAsync(doctorInsurance);
            }

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
            var doctors = await _context.Doctors.Where(d => ids.Contains(d.Id)).ToListAsync();

            if (doctors.Any())
            {
                _context.Doctors.RemoveRange(doctors);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Doctor>> SearchForDoctorAsync(int? specializationId, bool needsToBeAPediatrician, bool hasNZOK, int? regionId, int? insuranceId, string? firstName, string? lastName)
        {
            var query = _context.Doctors.AsQueryable();

            query = query.Where(d =>
                (!specializationId.HasValue || d.SpecializationId == specializationId.Value) &&
                (!needsToBeAPediatrician || d.IsPediatrician) &&
                (!hasNZOK || d.Nzok) &&
                (!regionId.HasValue || d.RegionId == regionId.Value) &&
                (!insuranceId.HasValue || d.InsuranceId == insuranceId.Value) &&
                (string.IsNullOrEmpty(firstName) || d.FirstName.Contains(firstName)) &&
                (string.IsNullOrEmpty(lastName) || d.LastName.Contains(lastName))
            );

            return await query.Include(d => d.Appointments).ToListAsync();
        }
    }
}
