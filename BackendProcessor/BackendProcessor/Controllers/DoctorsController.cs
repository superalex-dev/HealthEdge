using BackendProcessor.Models;
using BackendProcessor.Repositories;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet("doctors/get")]
        public async Task<IActionResult> GetDoctorsAsync()
        {
            IEnumerable<Doctor> doctors = await _doctorRepository.GetAllDoctors();

            if (doctors == null || doctors.Count() == 0)
            {
                return NoContent();
            }

            return Ok(doctors);
        }

        [HttpGet("doctors/get/{Id}")]
        public async Task<IActionResult> GetDoctorAsync(int Id)
        {
            Doctor doctor = await _doctorRepository.GetDoctor(Id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpPost("doctors/add")]
        public async Task<IActionResult> AddDoctorAsync([FromBody] Doctor doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }

            doctor.Id = 0;

            await _doctorRepository.AddDoctor(doctor);

            return CreatedAtAction(nameof(GetDoctorAsync), new { doctorId = doctor.Id }, doctor);
        }

        [HttpPut("doctors/edit/{Id}")]
        public async Task<IActionResult> EditDoctorAsync(int Id, [FromBody] Doctor doctor)
        {
            if (doctor == null || Id != doctor.Id)
            {
                return BadRequest();
            }

            await _doctorRepository.UpdateDoctor(doctor);

            return NoContent();
        }

        [HttpDelete("doctors/delete/{Id}")]
        public async Task<IActionResult> DeleteDoctorAsync(int Id)
        {
            await _doctorRepository.DeleteDoctor(Id);

            return NoContent();
        }
        
        [HttpDelete("doctors/delete-multiple")]
        public async Task<IActionResult> DeleteMultipleDoctorsAsync([FromBody] IEnumerable<int> ids)
        {
            await _doctorRepository.DeleteMultipleDoctors(ids);

            return NoContent();
        }
    }
}
