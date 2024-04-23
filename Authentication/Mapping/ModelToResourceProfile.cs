using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Authentication.Resources.GET;
using AutoMapper;

namespace AuthenticationService.Authentication.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Patient, PatientResource>();
    }
}