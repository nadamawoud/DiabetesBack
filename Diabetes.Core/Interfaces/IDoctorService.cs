using Diabetes.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorApprovalDto>> GetPendingDoctorsAsync();
        Task<DoctorApprovalDto> UpdateDoctorStatusAsync(int doctorId, string status);
    }

}
