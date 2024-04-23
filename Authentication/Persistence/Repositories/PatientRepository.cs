using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Authentication.Domain.Repositories;
using AuthenticationService.Shared.Persistence.Contexts;
using AuthenticationService.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Authentication.Persistence.Repositories;

public class PatientRepository : BaseRepository, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Patient>> ListAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> FindByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<Patient> FindByEmailAsync(string email)
    {
        return await _context.Patients.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Patient> FindByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Patients.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
    }

    public void Update(Patient patient)
    {
        _context.Update(patient);
    }

    public void Remove(Patient patient)
    {
        _context.Patients.Remove(patient);
    }
}