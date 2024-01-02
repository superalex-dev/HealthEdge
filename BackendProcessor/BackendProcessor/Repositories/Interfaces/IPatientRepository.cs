using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientByIdAsync(int Id);
        Task AddPatientAsync(Patient patient);
        Task<int> GetTotalPatientsCountAsync();
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
    }
}
