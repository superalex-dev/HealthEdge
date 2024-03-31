using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorAsync(int id);
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<ICollection<Doctor>> SearchForDoctorAsync(string specialization, bool needsToBeAPediatrician, string cityPreference);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
        Task DeleteMultipleDoctorsAsync(IEnumerable<int> ids);
    }
}
