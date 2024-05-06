using BackendProcessor.Data;
using BackendProcessor.Data.Dto;
using BackendProcessor.Models;
using BackendProcessor.Repositories;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BackendProcessor.Controllers
{
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly HospitalDbContext _context;

        public DoctorsController(IDoctorRepository doctorRepository, HospitalDbContext context)
        {
            _doctorRepository = doctorRepository;
            _context = context;
        }

        [HttpGet("doctors/get")]
        public async Task<IActionResult> GetDoctorsAsync()
        {
            IEnumerable<Doctor> doctors = await _doctorRepository.GetAllDoctorsAsync();

            if (doctors == null || doctors.Count() == 0)
            {
                return NoContent();
            }

            return Ok(doctors);
        }

        [HttpGet("doctors/get/{Id}")]
        public async Task<IActionResult> GetDoctorAsync(int Id)
        {
            Doctor doctor = await _doctorRepository.GetDoctorAsync(Id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpPost("doctors/create")]
        public async Task<ActionResult<DoctorDto>> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            var lastDoctor = await _context.Doctors
                .OrderByDescending(d => d.Username)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastDoctor != null && !string.IsNullOrEmpty(lastDoctor.Username))
            {
                var prefixLength = "healthedge".Length;

                if (lastDoctor.Username.Length > prefixLength)
                {
                    var lastNumberStr = lastDoctor.Username.Substring(prefixLength);
                    if (int.TryParse(lastNumberStr, out var lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
            }

            var now = DateTime.UtcNow;

            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = $"healthedge{nextNumber:0000}",
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RegionId = request.RegionId,
                IsPediatrician = request.IsPediatrician,
                SpecializationId = request.SpecializationId,
                Nzok = request.Nzok,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                DateOfCreation = request.DateOfCreation = now,
                ImageUrl = request.ImageUrl
            };

            await _doctorRepository.CreateDoctorAsync(doctor, request.InsuranceIds);

            var doctorDto = new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Username = doctor.Username,
                RegionId = doctor.RegionId,
                IsPediatrician = doctor.IsPediatrician,
                SpecializationId = doctor.SpecializationId,
                Nzok = doctor.Nzok,
                InsuranceIds = request.InsuranceIds,
                ContactNumber = doctor.ContactNumber,
                Email = doctor.Email,
                DateOfBirth = doctor.DateOfBirth,
                DateOfCreation = doctor.DateOfCreation,
                ImageUrl = doctor.ImageUrl
            };

            return Ok(doctorDto);
        }


        [HttpPut("doctors/edit/{Id}")]
        public async Task<IActionResult> EditDoctorAsync(int Id, [FromBody] Doctor doctor)
        {
            if (doctor == null || Id != doctor.Id)
            {
                return BadRequest();
            }

            await _doctorRepository.UpdateDoctorAsync(doctor);

            return Ok();
        }

        [HttpDelete("doctors/delete/{Id}")]
        public async Task<IActionResult> DeleteDoctorAsync(int Id)
        {
            await _doctorRepository.DeleteDoctorAsync(Id);

            return Ok();
        }
        
        [HttpDelete("doctors/delete-multiple")]
        public async Task<IActionResult> DeleteMultipleDoctorsAsync([FromBody] IEnumerable<int> ids)
        {
            await _doctorRepository.DeleteMultipleDoctorsAsync(ids);

            return Ok();
        }

        [HttpGet("doctors/search")]
        public async Task<IActionResult> SearchForDoctorAsync(
            [FromQuery] int? specializationId,
            [FromQuery] bool needsToBeAPediatrician,
            [FromQuery] bool hasNZOK,
            [FromQuery] int? regionId,
            [FromQuery] int? insuranceId,
            [FromQuery] string firstName,
            [FromQuery] string lastName)
        {
            if (!specializationId.HasValue && !regionId.HasValue && !insuranceId.HasValue && string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest("At least one search parameter must be provided.");
            }

            ICollection<Doctor> doctors = await _doctorRepository.SearchForDoctorAsync(specializationId, needsToBeAPediatrician, hasNZOK, regionId, firstName, lastName);

            if (doctors == null || doctors.Count == 0)
            {
                return NoContent();
            }

            var doctorDTOs = doctors.Select(d => new DoctorSearchResultDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                ImageUrl = d.ImageUrl,
                IsPediatrician = d.IsPediatrician,
                Nzok = d.Nzok,
                SpecializationId = d.SpecializationId,
                RegionId = d.RegionId,
                ContactNumber = d.ContactNumber,
                InsuranceIds = d.DoctorInsurances.Select(di => di.InsuranceId).ToList(),
                //InsuranceId = d.InsuranceId,
                Appointments = d.Appointments.Select(a => new AppointmentCreationDto
                {
                    Id = a.Id,
                    AppointmentTime = a.AppointmentTime,
                    Reason = a.Reason
                }).ToList()
            }).ToList();

            return Ok(doctorDTOs);
        }

        [HttpGet("doctors/specializations")]
        public async Task<IActionResult> GetSpecializations() 
        {
            var specializations = await _context.Specializations
                .Select(s => new { Id = s.Id, Name = s.Name })
                .ToListAsync();

            if (!specializations.Any())
            {
                return NoContent();
            }

            specializations = specializations
                .OrderBy(r => r.Name, new BulgarianStringComparer())
                .Distinct()
                .ToList();


            return Ok(specializations);
        }

        [HttpGet("doctors/cities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _context.Regions
                .Select(r => new { Id = r.Id, Name = r.Name })
                .ToListAsync();

            if (!cities.Any())
            {
                return NoContent();
            }

            cities = cities
                .OrderBy(r => r.Name, new BulgarianStringComparer())
                .Distinct()
                .ToList();

            return Ok(cities);
        }

        [HttpGet("doctors/insurances")]
        public async Task<IActionResult> GetInsurances()
        {
            var insurances = await _context.Insurance
                .Select(i => new { Id = i.Id, Name = i.Name })
                .ToListAsync();

            if (!insurances.Any())
            {
                return NoContent();
            }

            insurances = insurances
                .OrderBy(i => i.Name, new BulgarianStringComparer())
                .Distinct()
                .ToList();

            return Ok(insurances);
        }
    }
}

public class BulgarianStringComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        return string.Compare(x, y, new System.Globalization.CultureInfo("bg-BG"), System.Globalization.CompareOptions.StringSort);
    }
}
