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
                .RuleFor(d => d.ContactNumber, f => f.Phone.PhoneNumber())
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
    }
}
