using BackendProcessor.Data;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HealthEdgeDB;Integrated Security=True;");
        }
    }
}