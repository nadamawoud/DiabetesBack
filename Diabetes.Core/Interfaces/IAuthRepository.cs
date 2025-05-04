using Diabetes.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Diabetes.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<AppUser> CreateUserAsync(string email, string fullName, DateTime birthDate, string gender, string phoneNumber, string password);
        Task<bool> AssignRoleToUserAsync(AppUser user, string roleName);
        Task<AppUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
        Task<CasualUser> CreateCasualUserAsync(AppUser user, string verificationCode);
        Task<Clerk> CreateClerkAsync(AppUser user, string licenseCode, string verificationCode);
        Task<Doctor> CreateDoctorAsync(AppUser user, string specialization, string medicalSyndicateCardNumber);
        Task<bool> VerifyCasualUserEmailAsync(string email, string code);
        Task<bool> VerifyClerkEmailAsync(string email, string code);
    }
}