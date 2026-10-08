using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Company
{
    public class CompanyCreationDto
    {
        public string CompanyName { get; set; }
        public string? TVA { get; set; }
        public string? PhoneAreaCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
