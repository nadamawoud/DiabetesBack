using Diabetes.Core.Entities;
using Diabetes.Core.Interfaces;
using Diabetes.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Repository.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly StoreContext _context;

        public DoctorRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetPendingDoctorsAsync()
        {
            return await _context.Doctors
                .Where(d => d.DoctorApproval != null && d.DoctorApproval.ApprovalStatus == "Pending")
                .Include(d => d.AppUser)
                .Include(d => d.DoctorApproval)
                .ToListAsync();
        }

        public async Task<DoctorApproval> GetDoctorApprovalAsync(int doctorId)
        {
            return await _context.DoctorApprovals
                .FirstOrDefaultAsync(da => da.DoctorID == doctorId);
        }

        public async Task AcceptOrRejectDoctorAsync(int doctorId, string status)
        {
            var doctorApproval = await _context.DoctorApprovals
                .FirstOrDefaultAsync(da => da.DoctorID == doctorId);

            if (doctorApproval == null)
            {
                throw new Exception("Doctor approval record not found.");
            }

            doctorApproval.ApprovalStatus = status;
            doctorApproval.ApprovalDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }

}
