using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Therapist.Reposatories.Data;
using Therapist.Reposatories.Data.Identity;

namespace therapist.API.projectConfigrations.Databases
{
    public static class migrationServices
    {
        public static async Task MigrationService(IServiceProvider serviceProvider,WebApplication application)
        {
            var _db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var _identity = serviceProvider.GetRequiredService<IdentityDbContext>();
            var LoggerFactory= serviceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                await _db.Database.MigrateAsync();
                await _db.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "an error occured while migration");
            }
        }
    }
}
