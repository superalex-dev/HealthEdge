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
        public DbSet<VIPRoom> VIPRooms { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<HospitalDbContext>()
                .Build();

            string connectionString = configuration.GetConnectionString("HospitalDbConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.FirstName).IsRequired();
                entity.Property(u => u.LastName).IsRequired();
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.DateOfCreation).IsRequired();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.FirstName).IsRequired();
                entity.Property(d => d.LastName).IsRequired();
                entity.Property(d => d.Specialization).IsRequired();
                entity.Property(d => d.ContactNumber).IsRequired();
                entity.Property(d => d.Email).IsRequired();
            });
            
            modelBuilder.Entity<Patient>(entity =>
                    {
                        entity.HasKey(p => p.Id);
                        entity.HasOne(p => p.User)
                              .WithMany(u => u.Patients)
                              .HasForeignKey(p => p.UserId);
                        
                        entity.Property(p => p.FirstName).IsRequired();
                        entity.Property(p => p.LastName).IsRequired();
                        entity.Property(p => p.DateOfBirth).IsRequired();
                        entity.Property(p => p.Gender).IsRequired();
                        entity.Property(p => p.ContactNumber).IsRequired();
                        entity.Property(p => p.Address).IsRequired();
                    });
        }
    }
}