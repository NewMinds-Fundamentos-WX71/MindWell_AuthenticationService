using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Shared.Domain.Services.Communication;

namespace AuthenticationService.Authentication.Domain.Communication;

public class PatientResponse : BaseResponse<Patient>
{
    public PatientResponse(string message) : base(message)
    {
    }

    public PatientResponse(Patient resource) : base(resource)
    {
    }
}