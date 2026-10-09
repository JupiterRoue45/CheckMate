using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class CancellationPolicy
    {
        public int cancellationPolicyId { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public int? DeadlineHours { get; set; }
    }
}
