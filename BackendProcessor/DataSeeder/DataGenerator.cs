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
            "Акупунктура",
            "Акушер-гинеколог",
            "Акушерка",
            "Алерголог",
            "Алтернативни практики",
            "Ангиолог",
            "Анестезиолог",
            "Боуен терапевт",
            "Вирусолог",
            "Вътрешни болести",
            "Гастроентеролог",
            "Гръден хирург",
            "Дерматолог",
            "Детски гастроентеролог",
            "Детски ендокринолог",
            "Детски кардиолог",
            "Детски невролог",
            "Детски нефролог",
            "Детски психиатър",
            "Детски пулмолог",
            "Детски ревматолог",
            "Детски хематолог",
            "Детски хирург",
            "Диетолог",
            "Ендодонт",
            "Ендокринолог",
            "Естетичен дерматолог",
            "Зъболекар (Стоматолог)",
            "Изследване",
            "Имплантолог",
            "Имунолог",
            "Инфекциозни болести",
            "Кардиолог",
            "Кардиохирург",
            "Кинезитерапевт",
            "Клинична лаборатория",
            "Коуч",
            "Лицево-челюстен хирург",
            "Логопед",
            "Лъчетерапевт",
            "Мамолог",
            "Манипулация",
            "Медицинска генетика",
            "Медицинска сестра",
            "Микробиолог",
            "Невролог",
            "Неврохирург",
            "Неонатолог",
            "Нефролог (Бъбречни болести)",
            "Нуклеарна медицина",
            "Образна диагностика",
            "Общопрактикуващ лекар",
            "Озонотерапевт",
            "Онколог",
            "Оптометрист (Очен оптик)",
            "Орален хирург",
            "Ортодонт",
            "Ортопед",
            "Отоневролог",
            "Офталмолог (Очен лекар)",
            "Паразитолог",
            "Пародонтолог",
            "Педиатър",
            "Пластичен хирург",
            "Подиатър (Болести на ходилото)",
            "Протетик",
            "Профилактични прегледи",
            "Психиатър",
            "Психолог",
            "Психотерапевт",
            "Пулмолог (Белодробни болести)",
            "Ревматолог",
            "Репродуктивна медицина",
            "Рехабилитатор",
            "Спортна медицина",
            "Съдов хирург",
            "Токсиколог",
            "УНГ",
            "Уролог",
            "Физиотерапевт",
            "Хематолог (Клинична хематология)",
            "Хематолог (Трансфузионна хематология)",
            "Хирург",
            "Хомеопат",
            "Юмейхо терапевт"
        };

        static readonly List<string> insuranceFunds = new List<string>
        {
            "Аксиом",
            "Алианц",
            "Булстрад Живот",
            "България Иншурънс",
            "Групама",
            "Дженерали",
            "ДЗИ",
            "Доверие",
            "Евроинс",
            "ЕЗК (ЕЗОК)",
            "ЖЗИ",
            "МетЛайф",
            "Надежда",
            "ОЗОК Инс",
            "Съгласие",
            "Уника",
            "Фи Хелт",
            "ЦКБ Живот"
        };

        static readonly List<string> regions = new List<string>
        {
            "Blagoevgrad",
            "Burgas",
            "Varna",
            "Veliko Tarnovo",
            "Vidin",
            "Vratsa",
            "Gabrovo",
            "Dobrich",
            "Kardzhali",
            "Kyustendil",
            "Lovech",
            "Montana",
            "Pazardzhik",
            "Pernik",
            "Pleven",
            "Plovdiv",
            "Razgrad",
            "Ruse",
            "Silistra",
            "Sliven",
            "Smolyan",
            "Sofia",
            "Stara Zagora",
            "Targovishte",
            "Haskovo",
            "Shumen",
            "Yambol"
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


        public static List<Doctor> GenerateFullDoctors(int count, ICollection<Region> regions, ICollection<Specialization> specializations, ICollection<Insurance> insurances)
        {
            var faker = new Faker();
            var doctors = new List<Doctor>();

            foreach (var region in regions)
            {
                foreach (var specialization in specializations)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var doctorInsurances = faker.PickRandom(insurances, faker.Random.Int(1, insurances.Count)).ToList();
                        var doctor = new Doctor
                        {
                            FirstName = faker.Name.FirstName(),
                            LastName = faker.Name.LastName(),
                            Username = $"healthedge{++doctorUsernameSequence:0000}",
                            Password = BCrypt.Net.BCrypt.HashPassword(faker.Internet.Password()),
                            RegionId = region.Id,
                            IsPediatrician = faker.Random.Bool(),
                            SpecializationId = specialization.Id,
                            Nzok = faker.Random.Bool(),
                            ContactNumber = faker.Phone.PhoneNumber(),
                            Email = faker.Internet.Email(),
                            DateOfBirth = faker.Date.Past(30, DateTime.Now.AddYears(-30)),
                            DateOfCreation = DateTime.UtcNow,
                            ImageUrl = faker.Internet.Avatar(),
                            DoctorInsurances = doctorInsurances.Select(insurance => new DoctorInsurance { InsuranceId = insurance.Id }).ToList()
                        };
                        doctors.Add(doctor);
                    }
                }
            }

            return doctors;
        }

        public static List<Doctor> GenerateDoctorsInRegion(int count, int regionId, ICollection<Insurance> insurances, ICollection<Specialization> specializations)
        {
            if (insurances == null || insurances.Count == 0)
            {
                throw new ArgumentException("Insurances collection cannot be null or empty.", nameof(insurances));
            }

            if (specializations == null || specializations.Count == 0)
            {
                throw new ArgumentException("Specializations collection cannot be null or empty.", nameof(specializations));
            }

            var faker = new Faker();
            var doctors = new List<Doctor>();

            for (int i = 0; i < count; i++)
            {
                var doctorInsurances = faker.PickRandom(insurances, faker.Random.Int(1, insurances.Count)).ToList();
                var doctor = new Doctor
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Username = $"healthedge{++doctorUsernameSequence:0000}",
                    Password = BCrypt.Net.BCrypt.HashPassword(faker.Internet.Password()),
                    RegionId = regionId,
                    IsPediatrician = faker.Random.Bool(),
                    SpecializationId = faker.PickRandom(specializations).Id,
                    Nzok = faker.Random.Bool(),
                    ContactNumber = faker.Phone.PhoneNumber(),
                    Email = faker.Internet.Email(),
                    DateOfBirth = faker.Date.Past(30, DateTime.Now.AddYears(-30)),
                    DateOfCreation = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Local),
                    ImageUrl = faker.Internet.Avatar(),
                    DoctorInsurances = doctorInsurances.Select(insurance => new DoctorInsurance { InsuranceId = insurance.Id }).ToList()
                };
                doctors.Add(doctor);
            }

            return doctors;
        }

        public static List<Region> GenerateRegions()
        {
            var regionFaker = new Faker<Region>()
                .RuleFor(r => r.Name, f => f.PickRandom(regions));

            return regionFaker.Generate(regions.Count);
        }

        public static List<Specialization> GenerateSpecializations()
        {
            //generate specializations but alphabetically from specializations
            var specializations = medicalSpecializations.OrderBy(s => s).ToList();
            var specializationFaker = new Faker<Specialization>()
                .RuleFor(s => s.Name, f => specializations[f.IndexFaker]);

            return specializationFaker.Generate(specializations.Count);
        }

        public static List<Insurance> GenerateInsurances()
        {
            var insuranceFund = insuranceFunds.OrderBy(i => i).ToList();
            var insuranceFaker = new Faker<Insurance>()
                .RuleFor(i => i.Name, f => insuranceFund[f.IndexFaker]);

            return insuranceFaker.Generate(insuranceFunds.Count);
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
                .RuleFor(p => p.Email, f => f.Internet.Email());

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
