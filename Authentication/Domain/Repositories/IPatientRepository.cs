using AuthenticationService.Authentication.Domain.Models;

namespace AuthenticationService.Authentication.Domain.Repositories;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> ListAsync();
    Task<Patient> FindByIdAsync(int id);
    Task<Patient> FindByEmailAsync(string email);
    Task<Patient> FindByEmailAndPasswordAsync(string email, string password);
    Task AddAsync(Patient patient);
    void Update(Patient patient);
    void Remove(Patient patient);
}