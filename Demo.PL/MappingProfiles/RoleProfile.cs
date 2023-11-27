using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.MappingProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RolesViewModel>().ForMember(d=>d.RoleName, O=>O.MapFrom(s=>s.Name)).ReverseMap();
        }
    }
}
