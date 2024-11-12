using AutoMapper;
using ECommerceApi.Service.Base;

namespace ECommerceApi.Core.MediatrHandlers.Auth;

public class AuthMappingProfile: Profile
{
    public AuthMappingProfile()
    {
        CreateMap<AuthResult, AuthDto>();
    }
}
