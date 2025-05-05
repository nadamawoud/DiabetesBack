using Diabetes.Core.DTOs;
using Diabetes.Core.Entities;
using Diabetes.Core.Interfaces;
using Diabetes.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Services.Services
{
    public class DoctorAdminService : IDoctorAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreContext _context;

        public DoctorAdminService (UserManager<AppUser> userManager, StoreContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<DoctorAdminDto>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.AppUser)
                .Select(d => new DoctorAdminDto
                {
                    Id = d.ID,
                    FullName = d.AppUser.FullName,
                    Specialization = d.DoctorSpecialization,
                    Phone = d.AppUser.PhoneNumber,
                    Email = d.AppUser.Email
                })
                .ToListAsync();
        }

        public async Task<bool> AddDoctorAsync(CreateDoctorDto dto)
        {
            var appUser = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender
            };

            var result = await _userManager.CreateAsync(appUser, dto.Password);
            if (!result.Succeeded) return false;

            await _userManager.AddToRoleAsync(appUser, "Doctor");

            var doctor = new Doctor
            {
                AppUserId = appUser.Id,
                DoctorSpecialization = dto.Specialization,
                MedicalSyndicateCardNumber = dto.MedicalSyndicateCardNumber,
                IsApproved = true // Because added by admin
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateDoctorAsync(UpdateDoctorDto dto)
        {
            var doctor = await _context.Doctors
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(d => d.ID == dto.Id);
            if (doctor == null) return false;

            doctor.AppUser.FullName = dto.FullName;
            doctor.AppUser.PhoneNumber = dto.Phone;
            doctor.AppUser.Email = dto.Email;
            doctor.DoctorSpecialization = dto.Specialization;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(doctor.AppUser);
                await _userManager.ResetPasswordAsync(doctor.AppUser, token, dto.Password);
            }

            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(d => d.ID == id);
            if (doctor == null) return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            await _userManager.DeleteAsync(doctor.AppUser);
            return true;
        }
    }

}
