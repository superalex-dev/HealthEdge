﻿using BackendProcessor.Models;
using Bogus;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeeder
{
    public static class DataGenerator
    {
        private static int doctorUsernameSequence = 1;
        
        static readonly List<string> medicalSpecializations = new List<string>
        {
            "Cardiology",
            "Dermatology",
            "Emergency Medicine",
            "General Surgery",
            "Neurology",
            "Obstetrics and Gynecology",
            "Oncology",
            "Pediatrics",
            "Psychiatry",
            "Radiology"
        };
        public static List<Doctor> GenerateDoctors(int count)
        {
            var doctorFaker = new Faker<Doctor>()
                .RuleFor(d => d.FirstName, f => f.Name.FirstName())
                .RuleFor(d => d.LastName, f => f.Name.LastName())
                .RuleFor(d => d.Username, f => $"healthedge{doctorUsernameSequence++.ToString().PadLeft(4, '0')}")
                .RuleFor(d => d.Specialization, f => f.PickRandom(medicalSpecializations))
                .RuleFor(p => p.ContactNumber, f => f.Phone.PhoneNumber())
                .RuleFor(d => d.Email, f => f.Internet.Email());

            return doctorFaker.Generate(count);
        }

        public static List<Patient> GeneratePatients(int count, List<User> users)
        {
            var userIds = users.Select(u => u.Id).ToList();
            var patientFaker = new Faker<Patient>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.DateOfBirth, f => f.Date.Past(80))
                .RuleFor(p => p.Gender, f => f.PickRandom(new[] { "M", "F" }))
                .RuleFor(p => p.ContactNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Address, f => f.Address.FullAddress())
                .RuleFor(p => p.Email, f => f.Internet.Email())
                .RuleFor(p => p.UserId, f => f.PickRandom(userIds));

            return patientFaker.Generate(count);
        }

        public static List<Room> GenerateRooms(int count)
        {
            var roomFaker = new Faker<Room>()
                .RuleFor(r => r.RoomType, f => f.PickRandom(new[] { "Single", "Duo", "Standart", "Deluxe", "VIP" }))
                .RuleFor(r => r.IsOccupied, f => f.Random.Bool());

            var rooms = new List<Room>();

            for (int floor = 1; floor <= 9 && rooms.Count < count; floor++)
            {
                for (int roomsPerFloor = 1; roomsPerFloor <= 10 && rooms.Count < count; roomsPerFloor++)
                {
                    int roomNumber = floor * 100 + roomsPerFloor;
                    var room = roomFaker.Generate();
                    room.RoomNumber = roomNumber;
                    rooms.Add(room);
                }
            }

            return rooms;
        }
        public static List<User> GenerateUsers(int count)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: f.Internet.Password(),
                    salt: new byte[128 / 8],
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8)))
                .RuleFor(u => u.DateOfCreation, f => f.Date.Past(2).ToUniversalTime());

            return userFaker.Generate(count);
        }
    }
}
