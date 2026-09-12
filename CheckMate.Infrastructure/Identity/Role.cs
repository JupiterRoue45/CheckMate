using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Infrastructure.Identity
{
    public class Role : IdentityRole
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        public string? Description { get; set; }

    }
}
