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

        [HttpGet("patients/get/{id}")]
        public async Task<IActionResult> GetPatientAsync(int patientId)
        {
            Patient patient = await _patientRepository.GetPatient(patientId);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost("patients/add")]
        public async Task<IActionResult> AddPatientAsync([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }

            await _patientRepository.AddPatient(patient);

            return CreatedAtAction(nameof(GetPatientAsync), new { patientId = patient.PatientId }, patient);
        }

        [HttpPut("patients/edit/{id}")]
        public async Task<IActionResult> EditPatientAsync(int patientId, [FromBody] Patient patient)
        {
            if (patient == null || patientId != patient.PatientId)
            {
                return BadRequest();
            }

            await _patientRepository.UpdatePatient(patient);

            return NoContent();
        }

        [HttpDelete("patients/delete/{id}")]
        public async Task<IActionResult> DeletePatientAsync(int patientId)
        {
            await _patientRepository.DeletePatient(patientId);

            return NoContent();
        }
    }
}
