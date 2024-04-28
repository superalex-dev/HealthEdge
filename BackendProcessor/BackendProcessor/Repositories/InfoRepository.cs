using BackendProcessor.Data;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;

namespace BackendProcessor.Repositories
{
    public class InfoRepository : IInfoRepository
    {
        private readonly HospitalDbContext _context;

        public InfoRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Insurance> GetInsuranceAsync(int id)
        {
            return await _context.Insurance.FindAsync(id);
        }

        public async Task<Region> GetRegionAsync(int id)
        {
            return await _context.Regions.FindAsync(id);
        }

        public async Task<Specialization> GetSpecializationAsync(int id)
        {
            return await _context.Specializations.FindAsync(id);
        }
    }
}
