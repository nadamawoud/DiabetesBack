using AutoMapper;
using Diabetes.Core.Entities;
using Diabetes.Core.DTOs;

namespace Diabetes.API.Helpers
{

        public class MappingProfiles : Profile
        {
            public MappingProfiles()
            {
                CreateMap<AppUser, UserDto>()
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => GetUserRole(src)));
            }

            private static string GetUserRole(AppUser user)
            {
                if (user.Admin != null) return "Admin";
                if (user.Doctor != null) return "Doctor";
                if (user.Clerk != null) return "Clerk";
                if (user.CasualUser != null) return "CasualUser";
                if (user.Organization != null)
                    return user.Organization.IsMedicalSyndicate ? "MedicalSyndicate" : "Organization";

                return "Unknown";
            }
        }
    
}
