﻿using BackendProcessor.Data;
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

            await _context.Doctors.AddAsync(doctor);
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

        public async Task<ICollection<Doctor>> SearchForDoctorAsync(int? specializationId, bool needsToBeAPediatrician, int? regionId, string? firstName, string? lastName)
        {
            var query = _context.Doctors.AsQueryable();

            if (specializationId.HasValue)
            {
                query = query.Where(d => d.SpecializationId == specializationId.Value);
            }

            if (regionId.HasValue)
            {
                query = query.Where(d => d.RegionId == regionId.Value);
            }

            if (needsToBeAPediatrician)
            {
                query = query.Where(d => d.IsPediatrician);
            }

            if (!string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                query = query.Where(d => d.FirstName.Contains(firstName) || d.LastName.Contains(lastName));
            }

            return await query.Include(d => d.Appointments).ToListAsync();
        }
    }
}
