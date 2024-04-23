using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Authentication.Domain.Services;
using AuthenticationService.Authentication.Resources.GET;
using AuthenticationService.Authentication.Resources.POST;
using AuthenticationService.Authentication.Resources.PUT;
using AuthenticationService.Shared.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Authentication.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;

    public PatientsController(IPatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PatientResource>> GetAllAsync()
    {
        var patients = await _patientService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientResource>>(patients);
        
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<PatientResource> GetPatientbyId(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        var resource = _mapper.Map<Patient, PatientResource>(patient);
        
        return resource;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePatientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var patient = _mapper.Map<SavePatientResource, Patient>(resource);
        var result = await _patientService.SaveAsync(patient);

        if (!result.Success)
            return BadRequest(result.Message);

        var patientResource = _mapper.Map<Patient, PatientResource>(result.Resource);

        return Ok(patientResource);
    }
    
    [HttpPost("verify-patient")]
    public async Task<IActionResult> VerifyPatientAsync([FromBody] LoginResource loginResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var patient = await _patientService.GetByEmailAndPasswordAsync(loginResource.Email, loginResource.Password);

        if (patient == null)
        {
            return NotFound("Invalid credentials.");
        }

        var patientResource = _mapper.Map<Patient, PatientResource>(patient);

        return Ok(patientResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdatePatientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var patient = _mapper.Map<UpdatePatientResource, Patient>(resource);
        var result = await _patientService.UpdateAsync(id, patient);

        if (!result.Success)
            return BadRequest(result.Message);

        var patientResource = _mapper.Map<Patient, PatientResource>(result.Resource);

        return Ok(patientResource);
    }

    [HttpPut("update-password/{id}")]
    public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePatientPasswordResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var patient = _mapper.Map<UpdatePatientPasswordResource, Patient>(resource);
        var result = await _patientService.UpdatePasswordAsync(id, patient);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var patientResource = _mapper.Map<Patient, PatientResource>(result.Resource);
        
        return Ok(patientResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _patientService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var patientResource = _mapper.Map<Patient, PatientResource>(result.Resource);

        return Ok(patientResource);
    }
}