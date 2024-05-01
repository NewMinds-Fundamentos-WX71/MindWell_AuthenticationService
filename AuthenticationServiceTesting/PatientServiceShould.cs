using AuthenticationService.Authentication.Domain.Communication;
using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Authentication.Domain.Repositories;
using AuthenticationService.Authentication.Services;
using AuthenticationService.Shared.Persistence.Repositories;
using Moq;

namespace AuthenticationServiceTesting;

public class PatientServiceShould
{
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public PatientServiceShould()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsPatient()
    {
        // Arrange
        var expectedPatient = new Patient { Id = 1, Name = "John Doe" };
        _patientRepositoryMock.Setup(repo => repo.FindByIdAsync(1)).ReturnsAsync(expectedPatient);

        var patientService = new PatientService(_patientRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await patientService.GetByIdAsync(1);

        // Assert
        Assert.Equal(expectedPatient.Name, result.Name);
    }

    [Fact]
    public async Task SaveAsync_ReturnsPatientResponse()
    {
        // Arrange
        var patientToSave = new Patient { Name = "Jane Doe", Email = "jane@example.com", Password = "password" };
        var expectedResponse = new PatientResponse(patientToSave);

        _patientRepositoryMock.Setup(repo => repo.FindByEmailAsync(patientToSave.Email)).ReturnsAsync((Patient)null);
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        var patientService = new PatientService(_patientRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await patientService.SaveAsync(patientToSave);

        // Assert
        Assert.Equal(expectedResponse.Message, result.Message);
    }
}