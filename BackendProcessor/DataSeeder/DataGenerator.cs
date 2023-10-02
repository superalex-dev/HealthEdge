using BackendProcessor.Models;
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
                .RuleFor(p => p.ContactNumber, f => f.Phone.PhoneNumber())
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
                .RuleFor(p => p.ContactNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Address, f => f.Address.FullAddress());

            return patientFaker.Generate(count);
        }

        public static List<Room> GenerateRooms(int count)
        {
            var roomFaker = new Faker<Room>()
                .RuleFor(r => r.RoomNumber, f => GenerateRandomRoomNumber(f))
                .RuleFor(r => r.RoomType, f => f.PickRandom(new[] { "Standart", "Deluxe", "VIP" }))
                .RuleFor(r => r.IsOccupied, f => f.Random.Bool());

            return roomFaker.Generate(count);
        }

        static string GenerateRandomRoomNumber(Faker faker)
        {
            int floorNumber = faker.Random.Number(1, 10);
            int roomNumberOnFloor = faker.Random.Number(1, 20);

            return $"{floorNumber:D2}{roomNumberOnFloor:D2}";
        }

        public static List<User> GenerateUsers(int count)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(p => p.Password, f => f.Internet.Password())
                .RuleFor(u => u.DateOfCreation, f => f.Date.Past(2));

            return userFaker.Generate(count);
        }

        public static List<RoomCost> GenerateRoomCosts(int count)
        {
            var roomCostFaker = new Faker<RoomCost>()
                .RuleFor(r => r.RoomType, f => f.PickRandom(new[] { "Standart", "Deluxe", "VIP" }))
                .RuleFor(r => r.CostPerNight, f => f.Random.Number(100, 1000));

            return roomCostFaker.Generate(count);
        }
    }
}
