using BackendProcessor.Models;
using BackendProcessor.Repositories;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers
{
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
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
        public async Task<IActionResult> CreatePatientAsync([FromBody] Patient patient)
        {
            Patient createdPatient = await _patientRepository.CreatePatientAsync(patient);
            
            if (createdPatient == null)
            {
                return BadRequest();
            }
            
            return Ok(createdPatient);
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
    }
}
