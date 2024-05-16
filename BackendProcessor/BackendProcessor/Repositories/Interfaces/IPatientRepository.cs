using BackendProcessor.Data.Dto;
using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientByIdAsync(int Id);
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<int> GetTotalPatientsCountAsync();
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
        Task<List<Patient>> SearchPatientAsync(string patientName);
        Task<Patient> GetPatientByUsernameEmail(string username, string email);
        Task<List<Patient>> SearchPatientByDateOfBirthAsync(DateOnly patientDateOfBirth);
        Task<List<MedicalRecord>> GetPatientMedicalRecords(int partientId);
        Task<bool> AddPatientMedicalRecord(int patientId, MedicalRecordDto medicalRecordDto);
    }
}
