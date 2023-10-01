﻿using BackendProcessor.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeeder
{
    public static class DataGenerator
    {
        public static List<Doctor> GenerateDoctors(int count)
        {
            var doctorFaker = new Faker<Doctor>()
                .RuleFor(d => d.FirstName, f => f.Name.FirstName())
                .RuleFor(d => d.LastName, f => f.Name.LastName())
                .RuleFor(d => d.Specialization, f => f.Commerce.Department())
                .RuleFor(p => p.ContactNumber, f => GeneratePhoneNumber())
                .RuleFor(d => d.Email, f => f.Internet.Email());

            return doctorFaker.Generate(count);
        }

        public static List<Patient> GeneratePatients(int count)
        {
            var patientFaker = new Faker<Patient>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.DateOfBirth, f => f.Date.Past(80))
                .RuleFor(p => p.Gender, f => f.PickRandom(new[] { "M", "F" }))
                .RuleFor(p => p.ContactNumber, f => GeneratePhoneNumber())
                .RuleFor(p => p.Address, f => f.Address.FullAddress());

            return patientFaker.Generate(count);
        }

        public static string GeneratePhoneNumber()
        {
            var faker = new Faker();
            var phoneNumber = $"{faker.Phone.PhoneNumber()}";
            if (phoneNumber.Length > 20)
            {
                phoneNumber = phoneNumber.Substring(0, 20);
            }
            return phoneNumber;
        }

        //public static List<Room> GenerateRooms(int count)
        //{
        //    var roomFaker = new Faker<Room>()
        //        .RuleFor(r => r.RoomType, f => f.PickRandom(new[] { "Standart", "Deluxe", "VIP" }))
        //        .RuleFor(r => r.IsOccupied, f => f.Random.Bool());

        //    return roomFaker.Generate(count);
        //}

        //public static List<User> GenerateUsers(int count)
        //{
        //    var userFaker = new Faker<User>()
        //        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
        //        .RuleFor(u => u.LastName, f => f.Name.LastName())
        //        .RuleFor(u => u.UserName, f => f.Internet.UserName())
        //        .RuleFor(u => u.Email, f => f.Internet.Email())
        //        .RuleFor(u => u.DateOfCreation, f => f.Date.Past(2));

        //    return userFaker.Generate(count);
        //}
    }
}
