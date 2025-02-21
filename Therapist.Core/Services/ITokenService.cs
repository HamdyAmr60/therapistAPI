using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Core.Models.Identity;

namespace Therapist.Core.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(AppUser user , UserManager<AppUser> userManager);
    }
}
