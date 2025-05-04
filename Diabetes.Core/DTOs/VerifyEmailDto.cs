using System.ComponentModel.DataAnnotations;

namespace Diabetes.Core.DTOs
{
    public class VerifyEmailDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
