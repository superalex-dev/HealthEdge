﻿using BackendProcessor.Data;
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

            using var dbContext = new HospitalDbContext(optionsBuilder.Options);

            var doctors = DataGenerator.GenerateDoctors(10);
            var patients = DataGenerator.GeneratePatients(100);
            var rooms = DataGenerator.GenerateRooms(50);
            var users = DataGenerator.GenerateUsers(30);

            await dbContext.Doctors.AddRangeAsync(doctors);
            await dbContext.Patients.AddRangeAsync(patients);
            await dbContext.Rooms.AddRangeAsync(rooms);
            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            Console.WriteLine("Completed successfully!");
        }
    }
}