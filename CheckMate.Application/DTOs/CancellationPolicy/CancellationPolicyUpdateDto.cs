using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.CancellationPolicy
{
    public class CancellationPolicyUpdateDto
    {
        public string Code { get; set; }
        public string? Description { get; set; }
        public int? DeadlineHours { get; set; }
    }
}
