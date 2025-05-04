using Diabetes.Core.Entities;
using Diabetes.Core.Interfaces;
using Diabetes.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StoreContext _context;

        public AuthRepository(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            StoreContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<AppUser> CreateUserAsync(string email, string fullName, DateTime birthDate,
            string gender, string phoneNumber, string password)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                BirthDate = birthDate,
                Gender = gender,
                PhoneNumber = phoneNumber
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user;
        }

        public async Task<bool> AssignRoleToUserAsync(AppUser user, string roleName)
        {
            // لا نتحقق من وجود الدور لأنه مضاف مسبقاً في SQL
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                // تسجيل الخطأ للتحقق لاحقاً
                Console.WriteLine($"Failed to assign role: {string.Join(", ", result.Errors)}");
                return false;
            }

            return true;
        }

        // باقي الوظائف تبقى كما هي دون تغيير
        public async Task<AppUser> FindByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<bool> CheckPasswordAsync(AppUser user, string password) =>
            await _userManager.CheckPasswordAsync(user, password);

        public async Task<IList<string>> GetUserRolesAsync(AppUser user) =>
            await _userManager.GetRolesAsync(user);

        public async Task<CasualUser> CreateCasualUserAsync(AppUser user, string verificationCode)
        {
            var casualUser = new CasualUser
            {
                AppUserId = user.Id,
                VerificationCode = verificationCode,
                EmailVerified = false
            };

            await _context.CasualUsers.AddAsync(casualUser);
            await _context.SaveChangesAsync();
            return casualUser;
        }

        public async Task<Clerk> CreateClerkAsync(AppUser user, string licenseCode, string verificationCode)
        {
            var clerk = new Clerk
            {
                AppUserId = user.Id,
                LicenseCode = licenseCode,
                VerificationCode = verificationCode,
                IsEmailVerified = false
            };

            await _context.Clerks.AddAsync(clerk);
            await _context.SaveChangesAsync();
            return clerk;
        }

        public async Task<Doctor> CreateDoctorAsync(AppUser user, string specialization, string medicalSyndicateCardNumber)
        {
            var doctor = new Doctor
            {
                AppUserId = user.Id,
                DoctorSpecialization = specialization,
                MedicalSyndicateCardNumber = medicalSyndicateCardNumber,
                IsApproved = false
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> VerifyCasualUserEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var casualUser = await _context.CasualUsers.FirstOrDefaultAsync(c => c.AppUserId == user.Id);
            if (casualUser == null || casualUser.VerificationCode != code) return false;

            casualUser.EmailVerified = true;
            casualUser.VerificationCode = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VerifyClerkEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var clerk = await _context.Clerks.FirstOrDefaultAsync(c => c.AppUserId == user.Id);
            if (clerk == null || clerk.VerificationCode != code) return false;

            clerk.IsEmailVerified = true;
            clerk.VerificationCode = null;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}