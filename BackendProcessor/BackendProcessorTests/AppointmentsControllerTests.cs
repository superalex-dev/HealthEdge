using Xunit;
using Moq;
using BackendProcessor.Controllers;
using BackendProcessor.Repositories.Interfaces;
using System.Collections.Generic;
using BackendProcessor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackendProcessor.Data.Dto;

public class AppointmentsControllerTests
{
    [Fact]
    public async Task GetAppointments_ReturnsAllAppointments()
    {
        // Arrange
        var mockRepo = new Mock<IAppointmentRepository>();
        mockRepo.Setup(repo => repo.GetAllAppointmentsAsync())
            .ReturnsAsync(GetTestAppointments());
        var controller = new AppointmentsController(mockRepo.Object);

        // Act
        var result = await controller.GetAppointments();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var appointments = Assert.IsType<List<Appointment>>(actionResult.Value);
        Assert.Equal(2, appointments.Count);
    }

    [Fact]
    public async Task GetAppointment_ReturnsAppointment_WhenExists()
    {
        // Arrange
        var mockRepo = new Mock<IAppointmentRepository>();
        var testAppointment = new Appointment {Id = 1, DoctorId = 1, PatientId = 1};
        mockRepo.Setup(repo => repo.GetAppointmentByIdAsync(1))
            .ReturnsAsync(testAppointment);
        var controller = new AppointmentsController(mockRepo.Object);

        // Act
        var result = await controller.GetAppointment(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var appointment = Assert.IsType<Appointment>(actionResult.Value);
        Assert.Equal(1, appointment.Id);
    }

    [Fact]
    public async Task CreateAppointment_ReturnsCreatedAppointment()
    {
        // Arrange
        var mockRepo = new Mock<IAppointmentRepository>();
        var testAppointment = new Appointment {Id = 1, DoctorId = 1, PatientId = 1};
        mockRepo.Setup(repo => repo.CreateAppointmentAsync(It.IsAny<Appointment>()))
            .ReturnsAsync(testAppointment);
        var controller = new AppointmentsController(mockRepo.Object);
        var appointmentDto = new AppointmentCreationDto {PatientId = 1, DoctorId = 1};

        // Act
        var result = await controller.CreateAppointment(appointmentDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var appointment = Assert.IsType<Appointment>(actionResult.Value);
        Assert.Equal(1, appointment.Id);
    }

    [Fact]
    public async Task GetAppointmentsByDoctorId_ReturnsAllAppointmentsForDoctor()
    {
        // Arrange
        var mockRepo = new Mock<IAppointmentRepository>();
        mockRepo.Setup(repo => repo.GetAppointmentsByDoctorIdAsync(1))
            .ReturnsAsync(GetTestAppointments());
        var controller = new AppointmentsController(mockRepo.Object);

        // Act
        var result = await controller.GetAppointmentsByDoctorId(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var appointments = Assert.IsType<List<AppointmentCreationDto>>(actionResult.Value);
        Assert.Equal(2, appointments.Count);
    }

    [Fact]
    public async Task GetAppointmentsByPatientId_ReturnsAllAppointmentsForPatient()
    {
        // Arrange
        var mockRepo = new Mock<IAppointmentRepository>();
        mockRepo.Setup(repo => repo.GetAppointmentsByPatientIdAsync(1))
            .ReturnsAsync(GetTestAppointments());
        var controller = new AppointmentsController(mockRepo.Object);

        // Act
        var result = await controller.GetAppointmentsByPatientId(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var appointments = Assert.IsType<List<Appointment>>(actionResult.Value);
        Assert.Equal(2, appointments.Count);
    }

    [Fact]
    public async Task EditAppointment_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IAppointmentRepository>();
        var testAppointment = new Appointment {Id = 1, DoctorId = 1, PatientId = 1};
        mockRepo.Setup(repo => repo.EditAppointmentAsync(testAppointment))
            .Returns(Task.CompletedTask);
        var controller = new AppointmentsController(mockRepo.Object);

        // Act
        var result = await controller.EditAppointment(1, testAppointment);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteAppointment_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IAppointmentRepository>();
        mockRepo.Setup(repo => repo.DeleteAppointmentAsync(1))
            .Returns(Task.CompletedTask);
        var controller = new AppointmentsController(mockRepo.Object);

        // Act
        var result = await controller.DeleteAppointment(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    private List<Appointment> GetTestAppointments()
    {
        var appointments = new List<Appointment>();
        appointments.Add(new Appointment {Id = 1, DoctorId = 1, PatientId = 1});
        appointments.Add(new Appointment {Id = 2, DoctorId = 2, PatientId = 2});
        return appointments;
    }
}