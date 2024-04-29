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

        [HttpPost("patients/create")]
        public async Task<IActionResult> CreatePatientAsync([FromBody] PatientCreationDto patientDto)
        {
            var existingPatient = await _userRepository.GetUserByUsernameEmail(patientDto.UserName, patientDto.Email);
            var now = DateTime.UtcNow;

            if (existingPatient != null)
            {
                return Conflict("A user with this username or email already exists.");
            }

            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                UserName = patientDto.UserName,
                Email = patientDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(patientDto.Password),
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
                BloodType = patientDto.BloodType,
                ContactNumber = patientDto.ContactNumber,
                Address = patientDto.Address,
                DateOfCreation = now
            };

            var createdPatient = await _patientRepository.CreatePatientAsync(patient);

            var patientResponseDto = new PatientDto(
                createdPatient.Id,
                createdPatient.FirstName,
                createdPatient.LastName,
                createdPatient.UserName,
                createdPatient.Email,
                createdPatient.Password,
                createdPatient.DateOfBirth,
                createdPatient.Gender,
                createdPatient.BloodType,
                createdPatient.ContactNumber,
                createdPatient.Address,
                createdPatient.DateOfCreation
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

        [HttpGet("patients/searchByUserNameOrEmail")]
        public async Task<IActionResult> GetPatientByUserNameOrEmailAsync([FromQuery] string username, [FromQuery] string email)
        {
            var patient = await _patientRepository.GetPatientByUsernameEmail(username, email);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}
