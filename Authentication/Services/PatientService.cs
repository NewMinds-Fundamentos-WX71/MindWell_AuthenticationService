using AuthenticationService.Authentication.Domain.Communication;
using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Authentication.Domain.Repositories;
using AuthenticationService.Authentication.Domain.Services;
using AuthenticationService.Shared.Persistence.Repositories;

namespace AuthenticationService.Authentication.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Patient>> ListAsync()
    {
        return await _patientRepository.ListAsync();
    }

    public async Task<Patient> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _patientRepository.FindByEmailAndPasswordAsync(email, password);
    }

    public async Task<PatientResponse> SaveAsync(Patient patient)
    {
        try
        {
            var existingPatient = await _patientRepository.FindByEmailAsync(patient.Email);
            
            if (existingPatient != null)
                return new PatientResponse($"The email {patient.Email} already exists.");
            
            await _patientRepository.AddAsync(patient);
            await _unitOfWork.CompleteAsync();
            return new PatientResponse(patient);
        }
        catch (Exception e)
        {
            return new PatientResponse($"An error occurred while saving the patient: {e.Message}");
        }
    }

    public async Task<PatientResponse> UpdateAsync(int id, Patient patient)
    {
        try
        {
            var existingPatient = await _patientRepository.FindByIdAsync(id);
            
            if (existingPatient == null)
                return new PatientResponse("Patient not found.");
            
            existingPatient.Name = patient.Name;
            existingPatient.Age = patient.Age;
            
            _patientRepository.Update(existingPatient);
            await _unitOfWork.CompleteAsync();
            return new PatientResponse(existingPatient);
        }
        catch (Exception e)
        {
            return new PatientResponse($"An error occurred while updating the patient: {e.Message}");
        }
    }

    public async Task<PatientResponse> UpdatePasswordAsync(int id, Patient patient)
    {
        try
        {
            var existingPatient = await _patientRepository.FindByIdAsync(id);
            
            if (existingPatient == null)
                return new PatientResponse("Patient not found.");
            
            existingPatient.Password = patient.Password;
            
            _patientRepository.Update(existingPatient);
            await _unitOfWork.CompleteAsync();
            return new PatientResponse(existingPatient);
        }
        catch (Exception e)
        {
            return new PatientResponse($"An error occurred while updating the patient's password: {e.Message}");
        }
    }

    public async Task<PatientResponse> DeleteAsync(int id)
    {
        try
        {
            var existingPatient = await _patientRepository.FindByIdAsync(id);
            
            if (existingPatient == null)
                return new PatientResponse("Patient not found.");
            
            _patientRepository.Remove(existingPatient);
            await _unitOfWork.CompleteAsync();
            return new PatientResponse(existingPatient);
        }
        catch (Exception e)
        {
            return new PatientResponse($"An error occurred while deleting the patient: {e.Message}");
        }
    }
}