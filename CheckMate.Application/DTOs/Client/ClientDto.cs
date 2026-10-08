using CheckMate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClientTypeEnum Type { get; set; }
        public string? Email { get; set; }
        public string? PhoneAreaCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
