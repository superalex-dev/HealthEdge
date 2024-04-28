using BackendProcessor.Models;

namespace BackendProcessor.Repositories.Interfaces
{
    public interface IInfoRepository
    {
        Task<Specialization> GetSpecializationAsync(int id);
        Task<Region> GetRegionAsync(int id);
        Task<Insurance> GetInsuranceAsync(int id);

    }
}
