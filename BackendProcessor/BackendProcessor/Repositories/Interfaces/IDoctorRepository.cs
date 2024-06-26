﻿using BackendProcessor.Data.Dto;
using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<ICollection<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorAsync(int id);
        Task<Doctor> CreateDoctorAsync(Doctor doctor, IEnumerable<int> insuranceIds);
        Task<ICollection<Doctor>> SearchForDoctorAsync(int? specializationId, bool needsToBeAPediatrician, bool hasNZOK, int? regionId, string? firstName, string? lastName);
        //Task<ICollection<Doctor>> SearchForDoctorAsync(int? specializationId, bool needsToBeAPediatrician, bool hasNZOK, int? regionId, int? insuranceId, string? firstName, string? lastName);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
        Task DeleteMultipleDoctorsAsync(IEnumerable<int> ids);
        Task DeleteAllDoctorsAsync();
    }
}
