using BackendProcessor.Data;
using DataSeeder;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HealthEdgeDB;Integrated Security=True;");

            using var dbContext = new HospitalDbContext(optionsBuilder.Options);

            var patients = DataGenerator.GeneratePatients(100);

            await dbContext.Patients.AddRangeAsync(patients);
            await dbContext.SaveChangesAsync();

            Console.WriteLine("Completed successfully!");
        }
    }
}