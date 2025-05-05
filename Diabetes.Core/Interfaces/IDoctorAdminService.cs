using Diabetes.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Interfaces
{
    public interface IDoctorAdminService
    {
        Task<List<DoctorAdminDto>> GetAllDoctorsAsync();
        Task<bool> AddDoctorAsync(CreateDoctorDto dto);
        Task<bool> UpdateDoctorAsync(UpdateDoctorDto dto);
        Task<bool> DeleteDoctorAsync(int id);
    }

}
