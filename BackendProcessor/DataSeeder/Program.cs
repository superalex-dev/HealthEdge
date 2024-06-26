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

            await using var dbContext = new HospitalDbContext(optionsBuilder.Options);

            var insurances = dbContext.Insurance.ToList();
            var specializations = dbContext.Specializations.ToList();

            var doctors = DataGenerator.GenerateDoctorsInRegion(10, 23, insurances, specializations);

            foreach (var doctor in doctors)
            {
                dbContext.Doctors.Add(doctor);
                await dbContext.SaveChangesAsync();
            }

            //var regions = dbContext.Regions.ToList();
            //var specializations = dbContext.Specializations.ToList();
            //var insurances = dbContext.Insurance.ToList();

            //var doctors = DataGenerator.GenerateDoctors(10, regions, specializations, insurances);

            //foreach (var doctor in doctors)
            //{
            //    dbContext.Doctors.Add(doctor);
            //    await dbContext.SaveChangesAsync();
            //}

            //var doctors = DataGenerator.GenerateDoctors(48, regions, specializations, insurances);
            //await dbContext.Doctors.AddRangeAsync(doctors);
            //await dbContext.SaveChangesAsync();

            //var doctors = DataGenerator.GenerateDoctors(48);
            //var regions = DataGenerator.GenerateRegions();
            //var specializations = DataGenerator.GenerateSpecializations();
            //var insurance = DataGenerator.GenerateInsurances();
            //var users = DataGenerator.GenerateUsers(100);
            //await dbContext.Users.AddRangeAsync(users);
            //await dbContext.Regions.AddRangeAsync(regions);
            //await dbContext.Specializations.AddRangeAsync(specializations);
            //await dbContext.Insurance.AddRangeAsync(insurance);
            //await dbContext.SaveChangesAsync();
            //var patients = DataGenerator.GeneratePatients(100, users);
            //var rooms = DataGenerator.GenerateRooms(50);

            //await dbContext.Users.AddRangeAsync(users);
            //await dbContext.Doctors.AddRangeAsync(doctors);
            //await dbContext.SaveChangesAsync();

            //await dbContext.Patients.AddRangeAsync(patients);
            //await dbContext.SaveChangesAsync();

            //await dbContext.SaveChangesAsync();


            //await dbContext.Doctors.AddRangeAsync(doctors);
            //await dbContext.Patients.AddRangeAsync(patients);
            //await dbContext.Rooms.AddRangeAsync(rooms);

            //await dbContext.SaveChangesAsync();

            Console.WriteLine("Completed successfully!");
        }
    }
}