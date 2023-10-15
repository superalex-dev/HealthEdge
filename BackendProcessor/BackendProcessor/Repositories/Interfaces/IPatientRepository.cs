using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientByIdAsync(int Id);
        Task AddPatient(Patient patient);
        Task<int> GetTotalPatientsCountAsync();
        Task UpdatePatient(Patient patient);
        Task DeletePatient(int id);
    }
}
