using Diabetes.Core.DTOs;
using Diabetes.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Diabetes.API.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize(Roles = "Admin")]
    public class DoctorAdminController : ControllerBase
    {
        private readonly IDoctorAdminService _doctorService;

        public DoctorAdminController(IDoctorAdminService doctorService)
        {
            _doctorService = doctorService;
        }

        // 1) GET all doctors
        [HttpGet("getDoctors")]
        public async Task<ActionResult<List<DoctorAdminDto>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        // 2) POST new doctor
        [HttpPost("NewDoctors")]
        public async Task<ActionResult> AddDoctor([FromBody] CreateDoctorDto dto)
        {
            var result = await _doctorService.AddDoctorAsync(dto);
            if (!result) return BadRequest("Failed to create doctor.");
            return Ok("Doctor created successfully.");
        }

        // 3) PUT update doctor
        [HttpPut("UpdateDoctors/{id}")]
        public async Task<ActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch.");
            var result = await _doctorService.UpdateDoctorAsync(dto);
            if (!result) return NotFound("Doctor not found.");
            return Ok("Doctor updated successfully.");
        }

        // 4) DELETE doctor
        [HttpDelete("DeleteDoctors/{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            if (!result) return NotFound("Doctor not found.");
            return Ok("Doctor deleted successfully.");
        }
    }
}

