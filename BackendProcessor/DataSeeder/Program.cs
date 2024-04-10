using BackendProcessor.Data;
using DataSeeder;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
            optionsBuilder.UseNpgsql("HospitalDbConnection");

            await using var dbContext = new HospitalDbContext(optionsBuilder.Options);



            var doctors = DataGenerator.GenerateDoctors(48);
            var users = DataGenerator.GenerateUsers(100);
            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
            var patients = DataGenerator.GeneratePatients(100, users);
            var rooms = DataGenerator.GenerateRooms(50);

            await dbContext.Users.AddRangeAsync(users);
            await dbContext.Doctors.AddRangeAsync(doctors);
            await dbContext.SaveChangesAsync();

            await dbContext.Patients.AddRangeAsync(patients);
            await dbContext.SaveChangesAsync();

            await dbContext.SaveChangesAsync();


            await dbContext.Doctors.AddRangeAsync(doctors);
            await dbContext.Patients.AddRangeAsync(patients);
            await dbContext.Rooms.AddRangeAsync(rooms);

            await dbContext.SaveChangesAsync();

            Console.WriteLine("Completed successfully!");
        }
    }
}