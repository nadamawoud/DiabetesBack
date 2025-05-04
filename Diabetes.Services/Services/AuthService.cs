// Services/AuthService.cs
using Diabetes.Core.Entities;
using Diabetes.Core.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Diabetes.Core.Interfaces;
using Diabetes.Repository.Data;

namespace Diabetes.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StoreContext _context;
        private readonly IEmailService _emailService;

        public AuthService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            StoreContext context,
            IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _emailService = emailService;
        }

        public async Task<IdentityResult> RegisterClerkAsync(RegisterClerkDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Clerk");

                // الحصول على أول Admin في جدول Admins
                var firstAdminId = _context.Admins.OrderBy(a => a.ID).FirstOrDefault()?.ID;

                // إذا وجد Admin، تعيينه كـ AdminID
                if (firstAdminId != null)
                {
                    var verificationCode = GenerateVerificationCode();
                    var clerk = new Clerk
                    {
                        AppUserId = user.Id,
                        LicenseCode = registerDto.LicenseCode,
                        VerificationCode = verificationCode,
                        IsEmailVerified = false,
                        AdminID = firstAdminId.Value // تعيين AdminID من قاعدة البيانات
                    };

                    _context.Clerks.Add(clerk);
                    await _context.SaveChangesAsync();

                    // إرسال بريد التحقق
                    await _emailService.SendVerificationEmailAsync(user.Email, verificationCode);
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = "No admin available to assign." });
                }
            }

            return result;
        }

        public async Task<bool> VerifyClerkEmailAsync(VerifyEmailDto verifyDto)
        {
            var clerk = await _context.Clerks
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(c => c.AppUser.Email == verifyDto.Email);

            if (clerk == null || clerk.VerificationCode != verifyDto.Code)
                return false;

            clerk.IsEmailVerified = true;
            clerk.VerificationCode = null;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IdentityResult> RegisterCasualUserAsync(RegisterCasualUserDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FullName = registerDto.UserName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "CasualUser");

                // الحصول على أول Admin في جدول Admins
                var firstAdminId = _context.Admins.OrderBy(a => a.ID).FirstOrDefault()?.ID;

                // إذا وجد Admin، تعيينه كـ AdminID
                if (firstAdminId != null)
                {
                    var verificationCode = GenerateVerificationCode();
                    var casualUser = new CasualUser
                    {
                        AppUserId = user.Id,
                        VerificationCode = verificationCode,
                        EmailVerified = false,
                        AdminID = firstAdminId.Value // تعيين AdminID من قاعدة البيانات
                    };

                    _context.CasualUsers.Add(casualUser);
                    await _context.SaveChangesAsync();

                    await _emailService.SendVerificationEmailAsync(user.Email, verificationCode);
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = "No admin available to assign." });
                }
            }

            return result;
        }

        public async Task<bool> VerifyCasualUserEmailAsync(VerifyEmailDto verifyDto)
        {
            var casualUser = await _context.CasualUsers
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(c => c.AppUser.Email == verifyDto.Email);

            if (casualUser == null || casualUser.VerificationCode != verifyDto.Code)
                return false;

            casualUser.EmailVerified = true;
            casualUser.VerificationCode = null;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IdentityResult> RegisterDoctorAsync(RegisterDoctorDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Doctor");

                // الحصول على أول Admin في جدول Admins
                var firstAdminId = _context.Admins.OrderBy(a => a.ID).FirstOrDefault()?.ID;

                // إذا وجد Admin، تعيينه كـ AdminID
                if (firstAdminId != null)
                {
                    var doctor = new Doctor
                    {
                        AppUserId = user.Id,
                        DoctorSpecialization = registerDto.DoctorSpecialization,
                        MedicalSyndicateCardNumber = registerDto.MedicalSyndicateCardNumber,
                        IsApproved = false, // Needs approval from Medical Syndicate
                        AdminID = firstAdminId.Value // تعيين AdminID من قاعدة البيانات
                    };

                    _context.Doctors.Add(doctor);
                    await _context.SaveChangesAsync();

                    // هنا يمكنك إرسال إشعار إلى الـ Medical Syndicate عن تسجيل الطبيب الجديد
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = "No admin available to assign." });
                }
            }

            return result;
        }

        private string GenerateVerificationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}