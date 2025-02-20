using Microsoft.EntityFrameworkCore;
using Therapist.Reposatories.Data.Identity;

namespace therapist.API.projectConfigrations.Databases
{
    public static class IdentityConnections
    {
        public static WebApplicationBuilder  identityConniction(this WebApplicationBuilder applicationBuilder)
        {
          applicationBuilder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(applicationBuilder.Configuration.GetConnectionString("identity"));
            });
            return applicationBuilder;
        }
    }
}
