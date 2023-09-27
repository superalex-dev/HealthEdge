using Microsoft.EntityFrameworkCore;
using BackendProcessor.Models;

namespace BackendProcessor.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCost> RoomCosts { get; set; }
        public DbSet<VIPRoom> VIPRooms { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HealthEdgeDB;Integrated Security=True;");
        }

    }
}