using Diabetes.Core.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// IAuthService.cs in Core layer
namespace Diabetes.Core.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterClerkAsync(RegisterClerkDto registerDto);
        Task<bool> VerifyClerkEmailAsync(VerifyEmailDto verifyDto);
        Task<IdentityResult> RegisterCasualUserAsync(RegisterCasualUserDto registerDto);
        Task<bool> VerifyCasualUserEmailAsync(VerifyEmailDto verifyDto);
        Task<IdentityResult> RegisterDoctorAsync(RegisterDoctorDto registerDto);
    }
}
