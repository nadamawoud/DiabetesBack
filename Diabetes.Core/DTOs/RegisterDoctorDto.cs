using System.ComponentModel.DataAnnotations;

namespace Diabetes.Core.DTOs
{
    public class RegisterDoctorDto
    {
        public string FullName { get; set; }
        public string DoctorSpecialization { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string MedicalSyndicateCardNumber { get; set; }
        public string Password { get; set; }
    }
}
