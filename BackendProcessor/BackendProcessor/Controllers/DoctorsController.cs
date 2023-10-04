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

            await _doctorRepository.AddDoctor(doctor);

            return CreatedAtAction(nameof(GetDoctorAsync), new { doctorId = doctor.DoctorId }, doctor);
        }

        [HttpPut("doctors/edit/{Id}")]
        public async Task<IActionResult> EditDoctorAsync(int doctorId, [FromBody] Doctor doctor)
        {
            if (doctor == null || doctorId != doctor.DoctorId)
            {
                return BadRequest();
            }

            await _doctorRepository.UpdateDoctor(doctor);

            return NoContent();
        }

        [HttpDelete("doctors/delete/{Id}")]
        public async Task<IActionResult> DeleteDoctorAsync(int doctorId)
        {
            await _doctorRepository.DeleteDoctor(doctorId);

            return NoContent();
        }
    }
}
