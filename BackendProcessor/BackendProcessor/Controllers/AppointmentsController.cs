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

    [HttpGet("appointments/get")]
    public async Task<IActionResult> GetAppointmentsAsync()
    {
        IEnumerable<Appointment> appointments = await _appointmentRepository.GetAllAppointmentsAsync();

        if (appointments == null || appointments.Count() == 0)
        {
            return NoContent();
        }

        return Ok(appointments);
    }

    [HttpGet("appointments/get/{Id}")]
    public async Task<IActionResult> GetAppointmentAsync(int Id)
    {
        Appointment appointment = await _appointmentRepository.GetAppointmentAsync(Id);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }

    [HttpPost("appointments/create")]
    public async Task<IActionResult> CreateAppointmentAsync([FromBody] Appointment appointment)
    {
        Appointment createdAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);
        
        if (createdAppointment == null)
        {
            return BadRequest();
        }

        return Ok(createdAppointment);
    }

    [HttpPut("appointments/edit/{Id}")]
    public async Task<IActionResult> EditAppointmentAsync(int Id, [FromBody] Appointment appointment)
    {
        if (appointment == null || Id != appointment.Id)
        {
            return BadRequest();
        }

        Appointment updatedAppointment = await _appointmentRepository.EditAppointmentAsync(appointment);

        if (updatedAppointment == null)
        {
            return BadRequest();
        }

        return Ok(updatedAppointment);
    }
    
    [HttpDelete("appointments/delete/{Id}")]
    public async Task<IActionResult> DeleteAppointmentAsync(int Id)
    {
        await _appointmentRepository.DeleteAppointmentAsync(Id);

        return Ok();
    }
}