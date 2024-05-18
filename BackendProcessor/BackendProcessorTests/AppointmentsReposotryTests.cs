using BackendProcessor.Data;
using BackendProcessor.Models;
using BackendProcessor.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;
using BackendProcessor.Data.Dto;
using BackendProcessorTests;

public class PatientRepositoryTests
{
    private readonly Mock<HospitalDbContext> _mockContext;
    private readonly PatientRepository _repository;

    public PatientRepositoryTests()
    {
        _mockContext = new Mock<HospitalDbContext>();
        _repository = new PatientRepository(_mockContext.Object);
    }

    private List<Patient> GetTestPatients()
    {
        var patients = new List<Patient>();
        patients.Add(new Patient {Id = 1, FirstName = "John", LastName = "Doe"});
        patients.Add(new Patient {Id = 2, FirstName = "Jane", LastName = "Doe"});
        return patients;
    }

    [Fact]
    public async Task GetAllPatients_ReturnsAllPatients()
    {
        // Arrange
        _mockContext.Setup(c => c.Patients)
            .Returns(GetTestPatients().AsDbSetMock().Object);

        // Act
        var result = await _repository.GetAllPatients();

        // Assert
        Assert.Equal(GetTestPatients().Count, result.Count());
    }

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsPatient_WhenExists()
    {
        // Arrange
        var testPatient = new Patient {Id = 1, FirstName = "John", LastName = "Doe"};
        _mockContext.Setup(c => c.Patients.Find(1))
            .Returns(testPatient);

        // Act
        var result = await _repository.GetPatientByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreatePatientAsync_ReturnsCreatedPatient()
    {
        // Arrange
        var testPatient = new Patient {Id = 1, FirstName = "John", LastName = "Doe"};

        // Act
        var result = await _repository.CreatePatientAsync(testPatient);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetTotalPatientsCountAsync_ReturnsCorrectCount()
    {
        // Arrange
        _mockContext.Setup(c => c.Patients)
            .Returns(GetTestPatients().AsDbSetMock().Object);

        // Act
        var result = await _repository.GetTotalPatientsCountAsync();

        // Assert
        Assert.Equal(GetTestPatients().Count, result);
    }

    [Fact]
    public async Task UpdatePatientAsync_UpdatesPatient()
    {
        // Arrange
        var testPatient = new Patient {Id = 1, FirstName = "John", LastName = "Doe"};
        _mockContext.Setup(c => c.Patients.Find(1))
            .Returns(testPatient);

        // Act
        await _repository.UpdatePatientAsync(testPatient);

        // Assert
        _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeletePatientAsync_DeletesPatient()
    {
        // Arrange
        var testPatient = new Patient {Id = 1, FirstName = "John", LastName = "Doe"};
        _mockContext.Setup(c => c.Patients.Find(1))
            .Returns(testPatient);

        // Act
        await _repository.DeletePatientAsync(1);

        // Assert
        _mockContext.Verify(c => c.Patients.Remove(testPatient), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task SearchPatientAsync_ReturnsCorrectPatients()
    {
        // Arrange
        _mockContext.Setup(c => c.Patients)
            .Returns(GetTestPatients().AsDbSetMock().Object);

        // Act
        var result = await _repository.SearchPatientAsync("John");

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetPatientByUsernameEmail_ReturnsCorrectPatient()
    {
        // Arrange
        var testPatient = new Patient {Id = 1, UserName = "johndoe", Email = "john@doe.com"};
        _mockContext.Setup(c => c.Patients)
            .Returns(new List<Patient> { testPatient }.AsDbSetMock().Object);

        // Act
        var result = await _repository.GetPatientByUsernameEmail("johndoe", "john@doe.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task SearchPatientByDateOfBirthAsync_ReturnsCorrectPatients()
    {
        // Arrange
        var testDate = new DateOnly(2000, 1, 1);
        var testPatient = new Patient {Id = 1, DateOfBirth = testDate};
        _mockContext.Setup(c => c.Patients)
            .Returns(new List<Patient> { testPatient }.AsDbSetMock().Object);

        // Act
        var result = await _repository.SearchPatientByDateOfBirthAsync(testDate);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetPatientMedicalRecords_ReturnsCorrectRecords()
    {
        // Arrange
        var testRecord = new MedicalRecord {Id = 1, PatientId = 1};
        _mockContext.Setup(c => c.MedicalRecords)
            .Returns(new List<MedicalRecord> { testRecord }.AsDbSetMock().Object);

        // Act
        var result = await _repository.GetPatientMedicalRecords(1);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task AddPatientMedicalRecord_ReturnsTrue_WhenPatientExists()
    {
        // Arrange
        var testPatient = new Patient {Id = 1};
        var testRecordDto = new MedicalRecordDto {Id = 1, PatientId = 1};
        _mockContext.Setup(c => c.Patients)
            .Returns(new List<Patient> { testPatient }.AsDbSetMock().Object);

        // Act
        var result = await _repository.AddPatientMedicalRecord(1, testRecordDto);

        // Assert
        Assert.True(result);
    }
}