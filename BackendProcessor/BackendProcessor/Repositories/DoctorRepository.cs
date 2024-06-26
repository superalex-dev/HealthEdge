﻿using BackendProcessor.Data;
using BackendProcessor.Data.Dto;
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

        public async Task<ICollection<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors
                .Include(d => d.DoctorInsurances)
                .ThenInclude(di => di.Insurance)
                .ToListAsync();

            return doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Username = d.Username,
                Password = d.Password,
                RegionId = d.RegionId,
                IsPediatrician = d.IsPediatrician,
                SpecializationId = d.SpecializationId,
                Nzok = d.Nzok,
                InsuranceIds = d.DoctorInsurances.Select(di => di.InsuranceId).ToList(),
                ContactNumber = d.ContactNumber,
                Email = d.Email,
                DateOfBirth = d.DateOfBirth,
                DateOfCreation = d.DateOfCreation,
                ImageUrl = d.ImageUrl,
                Insurances = d.DoctorInsurances.Select(di => new InsuranceDto
                {
                    Id = di.Insurance.Id,
                    Name = di.Insurance.Name
                }).ToList()
            }).ToList();
        }

        public async Task<DoctorDto> GetDoctorAsync(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.DoctorInsurances)
                .ThenInclude(di => di.Insurance)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return null;
            }

            return new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Username = doctor.Username,
                Password = doctor.Password,
                RegionId = doctor.RegionId,
                IsPediatrician = doctor.IsPediatrician,
                SpecializationId = doctor.SpecializationId,
                Nzok = doctor.Nzok,
                InsuranceIds = doctor.DoctorInsurances.Select(di => di.InsuranceId).ToList(),
                ContactNumber = doctor.ContactNumber,
                Email = doctor.Email,
                DateOfBirth = doctor.DateOfBirth,
                DateOfCreation = doctor.DateOfCreation,
                ImageUrl = doctor.ImageUrl,
                Insurances = doctor.DoctorInsurances.Select(di => new InsuranceDto
                {
                    Id = di.Insurance.Id,
                    Name = di.Insurance.Name
                }).ToList()
            };
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor, IEnumerable<int> insuranceIds)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync(); // Save changes so that doctor.Id is generated

            foreach (var insuranceId in insuranceIds)
            {
                var existingDoctorInsurance = await _context.DoctorInsurances
                    .FirstOrDefaultAsync(di => di.DoctorId == doctor.Id && di.InsuranceId == insuranceId);

                if (existingDoctorInsurance == null)
                {
                    _context.DoctorInsurances.Add(new DoctorInsurance
                    {
                        DoctorId = doctor.Id,
                        InsuranceId = insuranceId
                    });
                }
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

        public async Task<ICollection<Doctor>> SearchForDoctorAsync(int? specializationId, bool needsToBeAPediatrician, bool hasNZOK, int? regionId, string? firstName, string? lastName)
        {
            if (_context == null || _context.Doctors == null)
            {
                return new List<Doctor>();
            }

            IQueryable<Doctor> query = _context.Doctors.AsQueryable();

            query = query.Where(d =>
                (!specializationId.HasValue || d.SpecializationId == specializationId.Value) &&
                (!needsToBeAPediatrician || d.IsPediatrician) &&
                (!hasNZOK || d.Nzok) &&
                (!regionId.HasValue || d.RegionId == regionId.Value) &&
                (string.IsNullOrEmpty(firstName) || d.FirstName.Contains(firstName)) &&
                (string.IsNullOrEmpty(lastName) || d.LastName.Contains(lastName))
            );

            try
            {
                var result = await query.Include(d => d.DoctorInsurances).ThenInclude(di => di.Insurance).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new List<Doctor>();
            }
        }

        public async Task DeleteAllDoctorsAsync()
        {
            foreach (var entity in _context.Doctors)
                _context.Doctors.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
