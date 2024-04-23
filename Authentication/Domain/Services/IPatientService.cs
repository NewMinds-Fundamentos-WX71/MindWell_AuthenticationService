using AuthenticationService.Authentication.Domain.Communication;
using AuthenticationService.Authentication.Domain.Models;

namespace AuthenticationService.Authentication.Domain.Services;

public interface IPatientService
{
    Task<IEnumerable<Patient>> ListAsync();
    Task<Patient> GetByIdAsync(int id);
    Task<Patient> GetByEmailAndPasswordAsync(string email, string password);
    Task<PatientResponse> SaveAsync(Patient patient);
    Task<PatientResponse> UpdateAsync(int id, Patient patient);
    Task<PatientResponse> UpdatePasswordAsync(int id, Patient patient);
    Task<PatientResponse> DeleteAsync(int id);
}