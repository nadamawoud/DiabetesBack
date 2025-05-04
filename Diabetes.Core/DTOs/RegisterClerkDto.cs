using System;
using System.ComponentModel.DataAnnotations;
using Diabetes.Core.Entities;
namespace Diabetes.Core.DTOs
{
   
        public class RegisterClerkDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public DateTime BirthDate { get; set; }
            public string Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string LicenseCode { get; set; }
            public string Password { get; set; }
        }
       
}
