using System.Collections;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers;

public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentsController(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        return Ok(await _appointmentRepository.GetAllAppointmentsAsync());
    }
    
    [HttpGet("{Id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int Id)
    {
        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(Id);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }
    
    [HttpGet("doctor/{doctorId}")]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDoctorId(int doctorId)
    {
        return Ok(await _appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId));
    }
    
    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByPatientId(int patientId)
    {
        return Ok(await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId));
    }
    
    [HttpPost]
    public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
    {
        var createdAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);
        return CreatedAtAction(nameof(GetAppointment), new { Id = createdAppointment.Id }, createdAppointment);
    }
    
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateAppointment(int Id, Appointment appointment)
    {
        if (Id != appointment.Id)
        {
            return BadRequest();
        }
        await _appointmentRepository.UpdateAppointmentAsync(appointment);
        return NoContent();
    }
    
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAppointment(int Id)
    {
        await _appointmentRepository.DeleteAppointmentAsync(Id);
        return NoContent();
    }
    
    [HttpGet("AvailableSlots/{doctorId}")]
    public async Task<ActionResult<IEnumerable<DateTime>>> GetAvailableSlots(int doctorId, [FromQuery] DateTime date)
    {
        if (date == null)
        {
            return BadRequest("Date is required.");
        }

        var availableSlots = await _appointmentRepository.GetAvailableSlots(doctorId, date);

        if (availableSlots == null || !availableSlots.Any())
        {
            return NotFound("No available slots for this date.");
        }

        return Ok(availableSlots);
    }
}