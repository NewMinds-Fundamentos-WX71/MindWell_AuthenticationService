using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Authentication.Resources.POST;
using AuthenticationService.Authentication.Resources.PUT;
using AutoMapper;

namespace AuthenticationService.Authentication.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SavePatientResource, Patient>();
        CreateMap<UpdatePatientResource, Patient>();
        CreateMap<UpdatePatientPasswordResource, Patient>();
    }
}