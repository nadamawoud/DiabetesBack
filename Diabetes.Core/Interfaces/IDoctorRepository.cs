using Diabetes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetPendingDoctorsAsync();
        Task<DoctorApproval> GetDoctorApprovalAsync(int doctorId);
        Task AcceptOrRejectDoctorAsync(int doctorId, string status);
    }

}
