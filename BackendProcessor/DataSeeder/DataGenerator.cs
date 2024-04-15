using BackendProcessor.Models;
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

        static readonly List<string> cities = new List<string>
        {
            "Sofia",
            "Plovdiv",
            "Varna",
            "Burgas",
            "Ruse",
            "Pleven",
            "Sliven",
            "Dobrich",
            "Shumen",
            "Pernik",
            "Haskovo",
            "Yambol",
            "Blagoevgrad",
            "Veliko Tarnovo",
            "Vratsa",
            "Gabrovo",
            "Asenovgrad",
            "Vidin",
            "Kazanlak",
            "Kyustendil",
            "Kardzhali",
            "Montana",
            "Lovech",
            "Silistra",
            "Targovishte",
            "Dupnitsa",
            "Svishtov",
            "Smolyan",
            "Petrich",
            "Samokov",
            "Lom",
            "Karlovo",
            "Sevlievo",
            "Nova Zagora",
            "Velingrad",
            "Cherven Bryag",
            "Troyan",
            "Aytos",
            "Byala Slatina",
            "Botevgrad",
            "Gotse Delchev",
            "Berkovitsa",
            "Pazardzhik",
            "Harmanli",
            "Karnobat",
            "Svilengrad",
            "Radomir",
            "Radnevo"
        };

        static readonly List<string> patientBloodTypes = new List<string>
        {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"
        };

        static readonly List<string> diagnosis = new List<string>
        {
            "Common cold",
            "Influenza",
            "Bronchitis",
            "Pneumonia",
            "Asthma",
            "Tuberculosis",
            "Hypertension",
            "Diabetes",
            "Hyperthyroidism",
            "Hypothyroidism",
            "Anemia",
            "Leukemia",
            "Lymphoma",
            "Melanoma",
            "Breast cancer",
            "Prostate cancer",
            "Colon cancer",
            "Lung cancer",
            "Heart attack",
            "Stroke",
            "Atrial fibrillation",
            "Heart failure",
            "Coronary artery disease",
            "Deep vein thrombosis",
            "Pulmonary embolism",
            "Cirrhosis",
            "Pancreatitis",
            "Gastritis",
            "Peptic ulcer disease",
            "Gastroesophageal reflux disease",
            "Irritable bowel syndrome",
            "Crohn's disease",
            "Ulcerative colitis",
            "Appendicitis",
            "Cholecystitis",
            "Hemorrhoids",
            "Varicose veins",
            "Osteoarthritis",
            "Rheumatoid arthritis",
            "Gout",
            "Osteoporosis",
            "Fibromyalgia",
            "Lupus",
            "Multiple sclerosis",
            "Parkinson's disease",
            "Alzheimer's disease",
            "Migraine",
            "Epilepsy",
            "Schizophrenia",
            "Bipolar disorder",
            "Depression",
            "Anxiety",
            "Obsessive-compulsive disorder",
            "Post-traumatic stress disorder",
            "Autism",
            "Attention deficit hyperactivity disorder",
            "Insomnia",
            "Sleep apnea",
            "Narcolepsy",
            "Restless legs syndrome",
            "Cataracts",
            "Glaucoma",
            "Macular degeneration",
            "Diabetic retinopathy",
            "Retinal detachment",
            "Conjunctivitis",
            "Keratitis",
            "Uveitis",
            "Retinitis pigmentosa",
            "Hearing loss",
            "Tinnitus",
            "Vertigo",
            "Meniere's disease",
            "Otitis media",
            "Sinusitis"
        };

        static readonly List<string> treatments = new List<string>
        {
            "Acetaminophen",
            "Ibuprofen",
            "Aspirin",
            "Naproxen",
            "Diclofenac",
            "Celecoxib",
            "Prednisone",
            "Methylprednisolone",
            "Hydrocortisone",
            "Dexamethasone",
            "Amoxicillin",
            "Azithromycin",
            "Ciprofloxacin",
            "Levofloxacin",
            "Metronidazole",
            "Clindamycin",
            "Cephalexin",
            "Ceftriaxone",
            "Cefixime",
            "Cefuroxime",
            "Cefdinir",
            "Cefaclor",
            "Cefprozil",
            "Cefepime",
            "Ceftazidime",
            "Ceftaroline",
            "Ceftazidime-avibactam",
            "Ceftolozane-tazobactam",
            "Ceftaroline-avibactam",
            "Ceftobiprole",
            "Ceftolozane",
            "Ceftibuten",
            "Cefditoren",
            "Cefpodoxime",
            "Cefixime",
            "Cefuroxime",
            "Cefdinir",
            "Cefaclor",
            "Cefprozil",
            "Cefepime",
            "Ceftazidime",
            "Ceftaroline",
            "Ceftazidime-avibactam",
            "Ceftolozane-tazobactam",
            "Ceftaroline-avibactam",
            "Ceftobiprole",
            "Ceftolozane",
            "Ceftibuten",
            "Cefditoren",
            "Cefpodoxime",
            "Cefixime",
            "Cefuroxime",
            "Cefdinir",
            "Cefaclor",
            "Cefprozil",
            "Cefepime",
            "Ceftazidime",
            "Ceftaroline",
            "Ceftazidime-avibactam",
            "Ceftolozane-tazobactam",
        };


        public static List<Doctor> GenerateDoctors(int count)
        {
            var doctorFaker = new Faker<Doctor>()
                .RuleFor(d => d.FirstName, f => f.Name.FirstName())
                .RuleFor(d => d.LastName, f => f.Name.LastName())
                .RuleFor(d => d.Username, f => $"healthedge{doctorUsernameSequence++.ToString().PadLeft(4, '0')}")
                .RuleFor(d => d.Specialization, f => f.PickRandom(medicalSpecializations))
                .RuleFor(p => p.ContactNumber, f => f.Phone.PhoneNumber())
                .RuleFor(d => d.Email, f => f.Internet.Email())
                .RuleFor(d => d.City, f => f.PickRandom(cities))
                .RuleFor(d => d.IsPediatrician, f => f.Random.Bool());

            return doctorFaker.Generate(count);
        }

        public static List<Patient> GeneratePatients(int count, ICollection<User> users)
        {
            var userIds = users.Select(u => u.Id).ToList();
            var patientFaker = new Faker<Patient>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.DateOfBirth, f => f.Date.PastDateOnly(80))
                .RuleFor(p => p.Gender, f => f.PickRandom(new[] { "M", "F" }))
                .RuleFor(p => p.BloodType, f => f.PickRandom(patientBloodTypes))
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

        // public static List<Appointment> GenerateAppointments(int count, ICollection<Patient> patients, List<Doctor> doctors, ICollection<Room> rooms)
        // {
        //     var appointmentFaker = new Faker<Appointment>()
        //         .RuleFor(a => a.RecordDate, f => f.Date.Future(1))
        //         .RuleFor(a => a.Diagnosis, f => f.PickRandom(diagnosis))
        //         .RuleFor(a => a.Treatment, f => f.PickRandom(treatments))
        //         .RuleFor(a => a.AppointmentDate, f => f.Date.Future(1))
        //         .RuleFor(a => a.PatientId, f => f.PickRandom(patients).Id)
        //          .RuleFor(a => a.DoctorId, f => f.PickRandom(doctors).Id)
        //         .RuleFor(a => a.RoomNumber, f => f.PickRandom(rooms).RoomNumber)
        //         .RuleFor(a => a.IsCancelled, f => f.Random.Bool())
        //         .RuleFor(a => a.IsCompleted, f => f.Random.Bool());
        //
        //     return appointmentFaker.Generate(count);
        // }
    }
}
