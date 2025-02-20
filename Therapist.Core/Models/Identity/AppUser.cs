using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.Core.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisName { get; set; }
    }
}
