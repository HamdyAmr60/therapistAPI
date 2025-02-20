using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Core.Models.Identity;

namespace Therapist.Reposatories.Data.Identity
{
    public class IdentityDbContext :IdentityDbContext<AppUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options):base(options)
        {
            
        }
    }
}
