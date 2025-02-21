using Microsoft.EntityFrameworkCore;
using Therapist.Reposatories.Data;

namespace therapist.API.projectConfigrations.Databases
{
    public static class ApplicationConnition
    {
        public static WebApplicationBuilder appDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("TherapistDb"));
            });
            return builder;
        }
    }
}
