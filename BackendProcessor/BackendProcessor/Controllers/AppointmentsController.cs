using System.Collections;
using System.Security.Policy;
using BackendProcessor.Data.Dto;
using BackendProcessor.Models;
using BackendProcessor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendProcessor.Controllers;

public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly TimeSpan _appointmentDuration = TimeSpan.FromMinutes(60);

    public AppointmentsController(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    [HttpGet("appointments/get")]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        return Ok(await _appointmentRepository.GetAllAppointmentsAsync());
    }
    
    [HttpGet("appointments/get/{Id}")]
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
        var appointments = await _appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId);

        var appointmentResponseDTO = new List<AppointmentCreationDto>();

        foreach (var appointment in appointments)
        {
            appointmentResponseDTO.Add(new AppointmentCreationDto
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentTime = appointment.AppointmentTime,
                Notes = appointment.Notes,
                Reason = appointment.Reason,
                PaymentMethod = appointment.PaymentMethod
            });
        }   

        return Ok(appointmentResponseDTO);
    }
    
    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByPatientId(int patientId)
    {
        return Ok(await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId));
    }

    [HttpPost("appointments/create")]
    public async Task<ActionResult<Appointment>> CreateAppointment(AppointmentCreationDto appointmentDto)
    {
        try
        {
            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                AppointmentTime = appointmentDto.AppointmentTime,
                Notes = appointmentDto.Notes,
                Reason = appointmentDto.Reason,
                PaymentMethod = appointmentDto.PaymentMethod
            };

            var createdAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { Id = createdAppointment.Id }, createdAppointment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while creating the appointment.");
        }
    }

    [HttpPut("appointments/edit/{Id}")]
    public async Task<IActionResult> EditAppointment(int Id, Appointment appointment)
    {
        if (Id != appointment.Id)
        {
            return BadRequest();
        }
        await _appointmentRepository.EditAppointmentAsync(appointment);
        return NoContent();
    }
    
    [HttpDelete("appointments/appointments/delete/{Id}")]
    public async Task<IActionResult> DeleteAppointment(int Id)
    {
        await _appointmentRepository.DeleteAppointmentAsync(Id);
        return NoContent();
    }

    [HttpGet("appointments/find-soonest-slot")]
    public async Task<IActionResult> FindSoonestSlot([FromQuery] int doctorId)
    {
        var appointment = await _appointmentRepository.FindSoonestAvailableAppointment(doctorId);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }
    
    //[HttpGet("doctor/{doctorId}/patients")]
    //public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByDoctorId(int doctorId)
    //{
    //    var patients = await _appointmentRepository.GetPatientsByDoctorIdAsync(doctorId);
    //    if (patients == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(patients);
    //}
}