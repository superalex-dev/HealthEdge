﻿using BackendProcessor.Data;
using BackendProcessor.Data.Dto;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace BackendProcessor.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int Id)
        {
            return await _context.Patients.FindAsync(Id);
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<int> GetTotalPatientsCountAsync()
        {
            return await _context.Patients.CountAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Patient>> SearchPatientAsync(string patientName)
        {
            var patients = await _context.Patients
                .Where(p => p.FirstName.Contains(patientName) || p.LastName.Contains(patientName))
                .ToListAsync();

            return patients;
        }

        public async Task<Patient> GetPatientByUsernameEmail(string username, string email)
        {
            return await _context.Patients
                .Where(u => u.UserName == username || u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Patient>> SearchPatientByDateOfBirthAsync(DateOnly patientDateOfBirth)
        {
            var patients = await _context.Patients
                .Where(p => p.DateOfBirth == patientDateOfBirth)
                .ToListAsync();

            return patients;
        }

        public async Task<List<MedicalRecord>> GetPatientMedicalRecords(int partientId)
        {
            var medicalRecords = await _context.MedicalRecords
                .Where(mr => mr.PatientId == partientId)
                .ToListAsync();
            
            return medicalRecords;
        }

        public async Task<bool> AddPatientMedicalRecord(int patientId, MedicalRecordDto medicalRecordDto)
        {
            var patient = await _context.Patients
                .Include(p => p.MedicalRecords)
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient != null)
            {
                var medicalRecord = new MedicalRecord
                {
                    Id = medicalRecordDto.Id,
                    DoctorId = medicalRecordDto.DoctorId,
                    PatientId = medicalRecordDto.PatientId,
                    RecordDate = medicalRecordDto.RecordDate,
                    Diagnosis = medicalRecordDto.Diagnosis,
                    Treatment = medicalRecordDto.Treatment
                };

                patient.MedicalRecords.Add(medicalRecord);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
