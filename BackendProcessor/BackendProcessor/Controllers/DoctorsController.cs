using BackendProcessor.Data;
using BackendProcessor.Models;
using BackendProcessor.Repositories;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> CreateDoctorAsync([FromBody] Doctor doctor)
        {
            Doctor createdDoctor = await _doctorRepository.CreateDoctorAsync(doctor);
            
            if (createdDoctor == null)
            {
                return BadRequest();
            }

            return Ok(createdDoctor);
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
            [FromQuery] int? regionId,
            [FromQuery] int? insuranceId,
            [FromQuery] string firstName,
            [FromQuery] string lastName)
        {
            if (!specializationId.HasValue && !regionId.HasValue && !insuranceId.HasValue && string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest("At least one search parameter must be provided.");
            }

            ICollection<Doctor> doctors = await _doctorRepository.SearchForDoctorAsync(specializationId, needsToBeAPediatrician, regionId, insuranceId, firstName, lastName);

            if (doctors == null || doctors.Count == 0)
            {
                return NoContent();
            }

            return Ok(doctors);
        }

        [HttpGet("doctors/specializations")]
        public async Task<IActionResult> GetSpecializations() 
        {
            var specializations = await _context.Doctors
                .Select(d => d.Specialization.Name)
                .Distinct()
                .ToListAsync();

            if (!specializations.Any())
            {
                return NoContent();
            }

            return Ok(specializations);
        }

        [HttpGet("doctors/cities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _context.Doctors
                .Select(d => d.Region.Name)
                .Distinct()
                .ToListAsync();

            if (!cities.Any())
            {
                return NoContent();
            }

            return Ok(cities);
        }

        [HttpGet("doctors/insurances")]
        public async Task<IActionResult> GetInsurances()
        {
            var insurances = await _context.Doctors
                .Select(d => d.Insurance.Name)
                .Distinct()
                .ToListAsync();

            if (!insurances.Any())
            {
                return NoContent();
            }

            return Ok(insurances);
        }
    }
}
