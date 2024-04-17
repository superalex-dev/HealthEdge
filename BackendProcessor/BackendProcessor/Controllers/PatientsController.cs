using BackendProcessor.Data.Dto;
using BackendProcessor.Models;
using BackendProcessor.Repositories;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;

        public PatientsController(IPatientRepository patientRepository, IUserRepository userRepository)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
        }

        [HttpGet("patients/get")]
        public async Task<IActionResult> GetPatientsAsync()
        {
            IEnumerable<Patient> patients = await _patientRepository.GetAllPatients();

            if (patients == null || patients.Count() == 0)
            {
                return NoContent();
            }

            return Ok(patients);
        }

        [HttpGet("patients/get/{Id}")]
        public async Task<IActionResult> GetPatientByIdAsync(int Id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(Id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost("patients/create/{userId}")]
        public async Task<IActionResult> CreatePatientAsync(int userId, [FromBody] PatientCreationDto patientDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Email = patientDto.Email,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                ContactNumber = patientDto.ContactNumber,
                Address = patientDto.Address,
                UserId = userId
            };

            var createdPatient = await _patientRepository.CreatePatientAsync(patient);

            var patientResponseDto = new PatientDto(
                createdPatient.Id,
                createdPatient.FirstName,
                createdPatient.LastName,
                createdPatient.Email,
                createdPatient.DateOfBirth,
                createdPatient.Gender,
                createdPatient.ContactNumber,
                createdPatient.Address,
                userId
            );

            if (patientResponseDto == null)
            {
                return BadRequest("Failed to create patient.");
            }

            return Ok(patientResponseDto);
        }

        [HttpGet("patients/count")]
        public async Task<IActionResult> GetPatientCount()
        {
            int count = await _patientRepository.GetTotalPatientsCountAsync();

            return Ok(count);
        }

        [HttpPut("patients/edit/{Id}")]
        public async Task<IActionResult> EditPatientAsync(int Id, [FromBody] Patient patient)
        {
            if (patient == null || Id != patient.Id)
            {
                return BadRequest();
            }

            await _patientRepository.UpdatePatientAsync(patient);

            return NoContent();
        }

        [HttpDelete("patients/delete/{Id}")]
        public async Task<IActionResult> DeletePatientAsync(int Id)
        {
            await _patientRepository.DeletePatientAsync(Id);

            return NoContent();
        }

        [HttpGet("patients/search/{patientName}")]
        public async Task<IActionResult> SearchPatientAsync(string patientName)
        {
            var patients = await _patientRepository.SearchPatientAsync(patientName);

            if (patients == null || patients.Count() == 0)
            {
                return NoContent();
            }

            return Ok(patients);
        }

        [HttpGet("patients/searchByDateOfBirth/{patientDateOfBirth}")]
        public async Task<IActionResult> SearchPatientByDateOfBirthAsync(DateOnly patientDateOfBirth)
        {
            var patients = await _patientRepository.SearchPatientByDateOfBirthAsync(patientDateOfBirth);

            if (patients == null || patients.Count() == 0)
            {
                return NoContent();
            }

            return Ok(patients);
        }
    }
}
